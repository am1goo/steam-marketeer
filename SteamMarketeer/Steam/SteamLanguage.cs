using System.Collections.Generic;

public class SteamLanguage
{
    private static readonly List<SteamLanguage> _all = new List<SteamLanguage>();
    public static IEnumerable<SteamLanguage> all => _all;

    private static readonly List<SteamLanguage> _ui = new List<SteamLanguage>();
    public static IEnumerable<SteamLanguage> ui => _ui;

    public static readonly SteamLanguage Arabic = Add(new SteamLanguage("Arabic", "العربية", "arabic", "ar", 0.00f, isUiSupported: false));
    public static readonly SteamLanguage Bulgarian = Add(new SteamLanguage("Bulgarian", "български език", "bulgarian", "bg", 0.04f));
    public static readonly SteamLanguage ChineseSimplified = Add(new SteamLanguage("Simplified Chinese", "简体中文", "schinese", "zh-CN", 31.69f));
    public static readonly SteamLanguage ChineseTraditional = Add(new SteamLanguage("Traditional Chinese", "繁體中文", "tchinese", "zh-TW", 1.38f));
    public static readonly SteamLanguage Czech = Add(new SteamLanguage("Czech", "čeština", "czech", "cs", 0.50f));
    public static readonly SteamLanguage Danish = Add(new SteamLanguage("Danish", "Dansk", "danish", "da", 0.22f));
    public static readonly SteamLanguage Dutch = Add(new SteamLanguage("Dutch", "Nederlands", "dutch", "nl", 0.25f));
    public static readonly SteamLanguage English = Add(new SteamLanguage("English", "English", "english", "en", 33.19f));
    public static readonly SteamLanguage Finnish = Add(new SteamLanguage("Finnish", "Suomi", "finnish", "fi", 0.14f));
    public static readonly SteamLanguage French = Add(new SteamLanguage("French", "Français", "french", "fr", 2.11f));
    public static readonly SteamLanguage German = Add(new SteamLanguage("German", "Deutsch", "german", "de", 2.76f));
    public static readonly SteamLanguage Greek = Add(new SteamLanguage("Greek", "Ελληνικά", "greek", "el", 0.06f));
    public static readonly SteamLanguage Hungarian = Add(new SteamLanguage("Hungarian", "Magyar", "hungarian", "hu", 0.35f));
    public static readonly SteamLanguage Indonesian = Add(new SteamLanguage("Indonesian", "Bahasa Indonesia", "indonesian", "id", 0.06f));
    public static readonly SteamLanguage Italian = Add(new SteamLanguage("Italian", "Italiano", "italian", "it", 0.59f));
    public static readonly SteamLanguage Japanese = Add(new SteamLanguage("Japanese", "日本語", "japanese", "ja", 2.41f));
    public static readonly SteamLanguage Korean = Add(new SteamLanguage("Korean", "한국어", "koreana", "ko", 1.46f));
    public static readonly SteamLanguage Norwegian = Add(new SteamLanguage("Norwegian", "Norsk", "norwegian", "no", 0.11f));
    public static readonly SteamLanguage Polish = Add(new SteamLanguage("Polish", "Polski", "polish", "pl", 1.57f));
    public static readonly SteamLanguage Portuguese = Add(new SteamLanguage("Portuguese - Portugal", "Português", "portuguese", "pt", 0.37f));
    public static readonly SteamLanguage PortugueseBrazil = Add(new SteamLanguage("Portuguese-Brazil", "Português-Brasil", "brazilian", "pt-BR", 3.64f));
    public static readonly SteamLanguage Romanian = Add(new SteamLanguage("Romanian", "Română", "romanian", "ro", 0.12f));
    public static readonly SteamLanguage Russian = Add(new SteamLanguage("Russian", "Русский", "russian", "ru", 9.16f));
    public static readonly SteamLanguage SpanishSpain = Add(new SteamLanguage("Spanish - Spain", "Español-España", "spanish", "es", 4.19f));
    public static readonly SteamLanguage SpanishLatinAmerica = Add(new SteamLanguage("Spanish - Latin America", "Español-Latinoamérica", "latam", "es-419", 0.44f));
    public static readonly SteamLanguage Swedish = Add(new SteamLanguage("Swedish", "Svenska", "swedish", "sv", 0.25f));
    public static readonly SteamLanguage Thai = Add(new SteamLanguage("Thai", "ไทย", "thai", "th", 0.88f));
    public static readonly SteamLanguage Turkish = Add(new SteamLanguage("Turkish", "Türkçe", "turkish", "tr", 1.27f));
    public static readonly SteamLanguage Ukrainian = Add(new SteamLanguage("Ukrainian", "Українська", "ukrainian", "uk", 0.64f));
    public static readonly SteamLanguage Vietnamese = Add(new SteamLanguage("Vietnamese", "Tiếng Việt", "vietnamese", "vn", 0.14f));

    private string _englishName;
    public string englishName => _englishName;

    private string _nativeName;
    public string nativeName => _nativeName;

    private string _apiLanguageCode;
    public string apiLanguageCode => _apiLanguageCode;

    private string _webApiLanguageCode;
    public string webApiLanguageCode => _webApiLanguageCode;

    private float _percentage;
    public float percentage => _percentage;

    private bool _isUiSupported;
    public bool isUiSupported => _isUiSupported;

    public SteamLanguage(string englishName, string nativeName, string apiLanguageCode, string webApiLanguageCode, float percentage) : this(englishName, nativeName, apiLanguageCode, webApiLanguageCode, percentage, isUiSupported: true)
    {
    }

    public SteamLanguage(string englishName, string nativeName, string apiLanguageCode, string webApiLanguageCode, float percentage, bool isUiSupported)
    {
        _englishName = englishName;
        _nativeName = nativeName;
        _apiLanguageCode = apiLanguageCode;
        _webApiLanguageCode = webApiLanguageCode;
        _percentage = percentage;
        _isUiSupported = isUiSupported;
    }

    public void Update(SteamSurvey.Language language)
    {
        _percentage = language.percentage;
    }

    private static SteamLanguage Add(SteamLanguage language)
    {
        _all.Add(language);
        if (language.isUiSupported)
            _ui.Add(language);
        return language;
    }

    public override string ToString()
    {
        return $"{_englishName} ({_webApiLanguageCode})";
    }
}