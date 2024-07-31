using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

public static class SteamSurvey
{
    public static async Task<List<Language>> GetLanguageStats(CancellationToken cancellationToken)
    {
        var result = new List<Language>();

        var uri = new Uri("https://store.steampowered.com/hwsurvey/Steam-Hardware-Software-Survey-Welcome-to-Steam");
        var content = await HttpUtility.GetContentAsync(uri, cancellationToken);

        var html = new HtmlDocument();
        html.LoadHtml(content);

        var stats = HtmlUtility.GetDivWithAttribute(html, "id", "main_stats");
        if (stats == null)
            return result;

        var languages = HtmlUtility.GetDivWithAttribute(stats, new HtmlUtility.Attribute("class", "stats_row_details"), new HtmlUtility.Attribute("id", "cat7_details"));

        var englishNames = new List<HtmlNode>();
        HtmlUtility.GetDivWithAttribute(languages, englishNames, "class", "stats_col_mid data_row");

        var percentages = new List<HtmlNode>();
        HtmlUtility.GetDivWithAttribute(languages, percentages, "class", "stats_col_right data_row");

        var changes = new List<HtmlNode>();
        HtmlUtility.GetDivWithAttribute(languages, changes, "class", "stats_col_right2 data_row");

        for (int i = 0; i < englishNames.Count; ++i)
        {
            if (!TryGetPercentage(percentages[i], out var percentage))
                continue;

            if (!TryGetPercentage(changes[i], out var change))
                continue;

            if (!TryGetString(englishNames[i], out var englishName))
                continue;

            var language = new Language(englishName, percentage, change);
            result.Add(language);
        }

        return result;
    }

    private static bool TryGetString(HtmlNode node, out string result)
    {
        foreach (var child in node.Descendants())
        {
            if (string.IsNullOrWhiteSpace(child.InnerText))
                continue;

            result = child.InnerText;
            return true;
        }

        result = default;
        return false;
    }

    private static bool TryGetPercentage(HtmlNode node, out float result)
    {
        foreach (var child in node.Descendants())
        {
            if (string.IsNullOrWhiteSpace(child.InnerText))
                continue;

            if (!child.InnerText.Contains('%'))
                continue;

            var innerText = child.InnerText.Replace("+", "").Replace("-", "").Replace("%", "");
            if (!float.TryParse(innerText, NumberStyles.Any, CultureInfo.InvariantCulture, out var parsed))
                continue;

            result = parsed;
            return true;
        }

        result = default;
        return false;
    }

    public class Language
    {
        private string _name;
        public string name => _name;

        private float _percentage;
        public float percentage => _percentage;

        private float _change;
        public float change => _change;

        public Language(string name, float percentage, float change)
        {
            _name = name;
            _percentage = percentage;
            _change = change;
        }

        public override string ToString()
        {
            return $"name={_name}, percentage={_percentage}, change={_change}";
        }
    }
}
