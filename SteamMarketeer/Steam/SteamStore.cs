using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public static class SteamStore
{
    public static async Task<Dictionary<SteamLanguage, string>> GetPagesAsync(uint appId, SteamUriType pageUri, IEnumerable<SteamLanguage> languages, CancellationToken cancellationToken)
    {
        var dict = new Dictionary<SteamLanguage, string>();

        var curIndex = 0;
        var loadedIndex = 0;
        var maxIndex = languages.Count();

        var tasks = new Task[maxIndex];
        foreach (var language in languages)
        {
            var task = GetPageAsync(appId, pageUri, language, cancellationToken);
            tasks[curIndex] = task;
            task.GetAwaiter().OnCompleted(() =>
            {
                lock (dict)
                {
                    loadedIndex++;

                    if (task.IsFaulted)
                    {
                        Logger.Error($"[ERROR] {task.Exception}");
                        return;
                    }

                    var content = task.Result;
                    dict.Add(language, content);
                    Logger.Log($"[{loadedIndex}/{maxIndex}] {pageUri} page on {language} language was loaded");
                }
            });
            curIndex++;
        }

        await TaskUtility.WaitAll(tasks);
        return dict;
    }

    public static async Task<string> GetPageAsync(uint appId, SteamUriType pageUri, SteamLanguage language, CancellationToken cancellationToken)
    {
        var uri = SteamUri.GetUri(appId, pageUri, language);
        return await HttpUtility.GetContentAsync(uri, cancellationToken);
    }
}
