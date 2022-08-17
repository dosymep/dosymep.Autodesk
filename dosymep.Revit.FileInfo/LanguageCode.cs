using System;
using System.Globalization;

namespace dosymep.Revit.FileInfo {
    /// <summary>
    /// Revit language code.
    /// </summary>
    public class LanguageCode {
        /// <summary>
        /// English - United States
        /// </summary>
        public static readonly LanguageCode ENU = new LanguageCode("ENU", CultureInfo.GetCultureInfo("en-US"));

        /// <summary>
        /// English - United Kingdom
        /// </summary>
        public static readonly LanguageCode ENG = new LanguageCode("ENG", CultureInfo.GetCultureInfo("en-GB"));

        /// <summary>
        /// French
        /// </summary>
        public static readonly LanguageCode FRA = new LanguageCode("FRA", CultureInfo.GetCultureInfo("fr-FR"));

        /// <summary>
        /// German
        /// </summary>
        public static readonly LanguageCode DEU = new LanguageCode("DEU", CultureInfo.GetCultureInfo("de-DE"));

        /// <summary>
        /// Italian
        /// </summary>
        public static readonly LanguageCode ITA = new LanguageCode("ITA", CultureInfo.GetCultureInfo("it-IT"));

        /// <summary>
        /// Japanese
        /// </summary>
        public static readonly LanguageCode JPN = new LanguageCode("JPN", CultureInfo.GetCultureInfo("ja-JP"));

        /// <summary>
        /// Korean
        /// </summary>
        public static readonly LanguageCode KOR = new LanguageCode("KOR", CultureInfo.GetCultureInfo("ko-KR"));

        /// <summary>
        /// Polish
        /// </summary>
        public static readonly LanguageCode PLK = new LanguageCode("PLK", CultureInfo.GetCultureInfo("pl-PL"));

        /// <summary>
        /// Spanish
        /// </summary>
        public static readonly LanguageCode ESP = new LanguageCode("ESP", CultureInfo.GetCultureInfo("es"));

        /// <summary>
        /// Simplified Chinese
        /// </summary>
        public static readonly LanguageCode CHS = new LanguageCode("CHS", CultureInfo.GetCultureInfo("zh-CN"));

        /// <summary>
        /// Traditional Chinese
        /// </summary>
        public static readonly LanguageCode CHT = new LanguageCode("CHT", CultureInfo.GetCultureInfo("zh-Hant"));

        /// <summary>
        /// Brazilian Portuguese
        /// </summary>
        public static readonly LanguageCode PTB = new LanguageCode("PTB", CultureInfo.GetCultureInfo("pt-BR"));

        /// <summary>
        /// Russian
        /// </summary>
        public static readonly LanguageCode RUS = new LanguageCode("RUS", CultureInfo.GetCultureInfo("ru-RU"));

        /// <summary>
        /// Czech
        /// </summary>
        public static readonly LanguageCode CSY = new LanguageCode("CSY", CultureInfo.GetCultureInfo("cs-CZ"));

        /// <summary>
        /// Unknown
        /// </summary>
        public static readonly LanguageCode Unknown = new LanguageCode("Unknown", CultureInfo.CurrentCulture);

        /// <summary>
        /// Returns language code by name.
        /// </summary>
        /// <param name="languageCode">Language code.</param>
        /// <returns>Returns language code by name.</returns>
        public static LanguageCode GetLanguageCode(string languageCode) {
            if(string.IsNullOrEmpty(languageCode)) {
                throw new ArgumentException("Value cannot be null or empty.", nameof(languageCode));
            }

            if(languageCode.Equals(ENU.Code) || languageCode.Equals("English_USA")) {
                return ENU;
            } else if(languageCode.Equals(ENG.Code) || languageCode.Equals("English_GB")) {
                return ENG;
            } else if(languageCode.Equals(FRA.Code) || languageCode.Equals("French")) {
                return FRA;
            } else if(languageCode.Equals(DEU.Code) || languageCode.Equals("German")) {
                return DEU;
            } else if(languageCode.Equals(ITA.Code) || languageCode.Equals("Italian")) {
                return ITA;
            } else if(languageCode.Equals(JPN.Code) || languageCode.Equals("Japanese")) {
                return JPN;
            } else if(languageCode.Equals(KOR.Code) || languageCode.Equals("Korean")) {
                return KOR;
            } else if(languageCode.Equals(PLK.Code) || languageCode.Equals("Polish")) {
                return PLK;
            } else if(languageCode.Equals(ESP.Code) || languageCode.Equals("Spanish")) {
                return ESP;
            } else if(languageCode.Equals(CHS.Code) || languageCode.Equals("Chinese_Simplified")) {
                return CHS;
            } else if(languageCode.Equals(CHT.Code) || languageCode.Equals("Chinese_Traditional")) {
                return CHT;
            } else if(languageCode.Equals(PTB.Code) || languageCode.Equals("Brazilian_Portuguese")) {
                return PTB;
            } else if(languageCode.Equals(RUS.Code) || languageCode.Equals("Russian")) {
                return RUS;
            } else if(languageCode.Equals(CSY.Code) || languageCode.Equals("Czech")) {
                return CSY;
            }

            throw new NotSupportedException($"The {languageCode} is not supported.");
        }

        /// <summary>
        /// Creates language code object. 
        /// </summary>
        /// <param name="code">Revit language code value.</param>
        /// <param name="cultureInfo">Culture info.</param>
        private LanguageCode(string code, CultureInfo cultureInfo) {
            Code = code;
            CultureInfo = cultureInfo;
        }

        /// <summary>
        /// Language code value.
        /// </summary>
        public string Code { get; }

        /// <summary>
        /// Current culture info.
        /// </summary>
        public CultureInfo CultureInfo { get; }

        /// <summary>
        /// Display name.
        /// </summary>
        public string DisplayName => CultureInfo?.DisplayName;

        /// <summary>
        /// Display name.
        /// </summary>
        public string EnglishName => CultureInfo.EnglishName;
    }
}