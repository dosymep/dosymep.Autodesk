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
        /// Hungarian
        /// </summary>
        public static readonly LanguageCode HUN = new LanguageCode("Hun", CultureInfo.GetCultureInfo("hu-HU"));

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

            if(languageCode.Equals(ENU.Code, StringComparison.CurrentCultureIgnoreCase)
               || languageCode.Equals("English_USA", StringComparison.CurrentCultureIgnoreCase)) {
                return ENU;
            } else if(languageCode.Equals(ENG.Code, StringComparison.CurrentCultureIgnoreCase)
                      || languageCode.Equals("English_GB", StringComparison.CurrentCultureIgnoreCase)) {
                return ENG;
            } else if(languageCode.Equals(FRA.Code, StringComparison.CurrentCultureIgnoreCase)
                      || languageCode.Equals("French", StringComparison.CurrentCultureIgnoreCase)) {
                return FRA;
            } else if(languageCode.Equals(DEU.Code, StringComparison.CurrentCultureIgnoreCase)
                      || languageCode.Equals("German", StringComparison.CurrentCultureIgnoreCase)) {
                return DEU;
            } else if(languageCode.Equals(ITA.Code, StringComparison.CurrentCultureIgnoreCase)
                      || languageCode.Equals("Italian", StringComparison.CurrentCultureIgnoreCase)) {
                return ITA;
            } else if(languageCode.Equals(JPN.Code, StringComparison.CurrentCultureIgnoreCase)
                      || languageCode.Equals("Japanese", StringComparison.CurrentCultureIgnoreCase)) {
                return JPN;
            } else if(languageCode.Equals(KOR.Code, StringComparison.CurrentCultureIgnoreCase)
                      || languageCode.Equals("Korean", StringComparison.CurrentCultureIgnoreCase)) {
                return KOR;
            } else if(languageCode.Equals(PLK.Code, StringComparison.CurrentCultureIgnoreCase)
                      || languageCode.Equals("Polish", StringComparison.CurrentCultureIgnoreCase)) {
                return PLK;
            } else if(languageCode.Equals(ESP.Code, StringComparison.CurrentCultureIgnoreCase)
                      || languageCode.Equals("Spanish", StringComparison.CurrentCultureIgnoreCase)) {
                return ESP;
            } else if(languageCode.Equals(CHS.Code, StringComparison.CurrentCultureIgnoreCase)
                      || languageCode.Equals("Chinese_Simplified", StringComparison.CurrentCultureIgnoreCase)) {
                return CHS;
            } else if(languageCode.Equals(CHT.Code, StringComparison.CurrentCultureIgnoreCase)
                      || languageCode.Equals("Chinese_Traditional", StringComparison.CurrentCultureIgnoreCase)) {
                return CHT;
            } else if(languageCode.Equals(PTB.Code, StringComparison.CurrentCultureIgnoreCase)
                      || languageCode.Equals("Brazilian_Portuguese", StringComparison.CurrentCultureIgnoreCase)) {
                return PTB;
            } else if(languageCode.Equals(RUS.Code, StringComparison.CurrentCultureIgnoreCase)
                      || languageCode.Equals("Russian", StringComparison.CurrentCultureIgnoreCase)) {
                return RUS;
            } else if(languageCode.Equals(CSY.Code, StringComparison.CurrentCultureIgnoreCase)
                      || languageCode.Equals("Czech", StringComparison.CurrentCultureIgnoreCase)) {
                return CSY;
            }else if(languageCode.Equals(HUN.Code, StringComparison.CurrentCultureIgnoreCase)
                     || languageCode.Equals("Hungarian", StringComparison.CurrentCultureIgnoreCase)) {
                return HUN;
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