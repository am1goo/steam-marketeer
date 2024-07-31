using System;
using System.Collections.Generic;

public static class SteamUri
{
    public static Uri GetUri(uint appId, SteamUriType pageUri, SteamLanguage language = null)
    {
        string baseUrl;
        switch (pageUri)
        {
            case SteamUriType.Store:
                baseUrl = $"https://store.steampowered.com/app/{appId}";
                break;

            case SteamUriType.Community:
                baseUrl = $"https://steamcommunity.com/app/{appId}";
                break;

            default:
                throw new Exception($"unsupported type {pageUri}");
        }

        var query = new List<string>();
        if (language != null)
            query.Add($"l={language.apiLanguageCode}");

        var url = baseUrl;
        if (query.Count > 0)
            url += $"?{string.Join('&', query)}";
        return new Uri(url);
    }
}
