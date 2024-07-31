using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SteamMarketeer
{
    class Program
    {
        private const string ASCII_NAME =
@"
 +-+-+-+-+-+ +-+-+-+-+-+-+-+-+-+
 |S|T|E|A|M| |M|A|R|K|E|T|E|E|R|
 +-+-+-+-+-+ +-+-+-+-+-+-+-+-+-+
";

        static async Task Main(string[] args)
        {
            var thisName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            var thisVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

            Logger.Log(ASCII_NAME);
            Logger.Log($"Version: {thisVersion}");
            Logger.Log($"License: MIT");
            Logger.Skip();

            if (args.Length <= 0 || !uint.TryParse(args[0], out var appId))
            {
                Logger.Log("The syntax of the command is incorrect.");
                Logger.Skip();
                Logger.Log($"Usage: {thisName}.exe [APP_ID]");
                Logger.Skip();
                Logger.Log("APP_ID - it's app id from Steam Store (you can see these numbers in any game url)");
                return;
            }

            Logger.Log($"Collecting information about languages in Steam");
            var survey = await SteamSurvey.GetLanguageStats(cancellationToken: default);
            foreach (var language in survey)
            {
                var target = SteamLanguage.all.FirstOrDefault(x => string.Equals(x.englishName, language.name, StringComparison.InvariantCultureIgnoreCase));
                if (target == null)
                {
                    Logger.Error($"language {language.name} not found to update");
                    continue;
                }

                target.Update(language);
            }

            Logger.Log($"Collecting all information about appId={appId}");
            var baseLanguage = SteamLanguage.English;
            var allLanguages = SteamLanguage.ui;

            var pages = await SteamStore.GetPagesAsync(appId, SteamUriType.Store, allLanguages, cancellationToken: default);
            var htmls = new Dictionary<SteamLanguage, HtmlDocument>();
            foreach (var page in pages)
            {
                var language = page.Key;
                var content = page.Value;

                var html = new HtmlDocument();
                html.LoadHtml(content);
                htmls.Add(language, html);
            }

            if (!htmls.TryGetValue(baseLanguage, out var baseHtml))
            {
                Logger.Log($"Base {baseLanguage} language is not loaded and can't be used");
                return;
            }

            var noLanguages = new List<SteamLanguage>();
            var uniqueLanguages = new List<SteamLanguage>()
            {
                baseLanguage
            };

            var baseAppName = HtmlUtility.GetDivWithAttribute(baseHtml, "class", "apphub_AppName")?.InnerText;

            var baseShortDescription = HtmlUtility.GetDivWithAttribute(baseHtml, "class", "game_description_snippet")?.InnerText;
            foreach (var otherLanguage in htmls.Keys)
            {
                if (otherLanguage == baseLanguage)
                    continue;

                var otherHtml = htmls[otherLanguage];
                var otherShortDescription = HtmlUtility.GetDivWithAttribute(otherHtml, "class", "game_description_snippet")?.InnerText;

                if (string.Equals(baseShortDescription, otherShortDescription))
                {
                    noLanguages.Add(otherLanguage);
                }
                else
                {
                    uniqueLanguages.Add(otherLanguage);
                }
            }

            var appName = baseAppName ?? appId.ToString();
            Logger.Log($"Game '{appName}' (appId={appId}) store page supports {uniqueLanguages.Count} languages.");
            Logger.Log($"Report:");
            foreach (var language in allLanguages)
            {
                if (noLanguages.Contains(language))
                {
                    Logger.Log($"Missed: {language}", ConsoleColor.Red);
                }
                else if (uniqueLanguages.Contains(language))
                {
                    Logger.Log($"Found: {language}");
                }
            }

            if (noLanguages.Count == 0)
            {
                Logger.Log("Store page contains all languages, everything is okay!", ConsoleColor.Green);
            }
            else
            {
                var noPercent = noLanguages.Sum(x => x.percentage);
                var allPercent = allLanguages.Sum(x => x.percentage);
                var coverPercent = (int)Math.Round(Math.Clamp(1f - noPercent / (float)allPercent, 0f, 1f) * 100);
                Logger.Log($"Store page does not contains all languages and covers only {coverPercent}% of all users, please take a look at this!", ConsoleColor.Yellow);
                Logger.Log($"(based on information from https://store.steampowered.com/hwsurvey/Steam-Hardware-Software-Survey-Welcome-to-Steam)", ConsoleColor.Yellow);
            }
#if DEBUG
            Console.ReadKey();
#endif
        }
    }
}
