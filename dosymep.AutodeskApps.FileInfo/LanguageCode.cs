using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace dosymep.AutodeskApps.FileInfo {
    /// <summary>
    /// Autodesk language code.
    /// </summary>
    public class LanguageCode : IEquatable<LanguageCode> {
        /// <summary>
        /// English - United States
        /// </summary>
        public static readonly LanguageCode ENU
            = new LanguageCode("ENU", "English_USA", CultureInfo.GetCultureInfo("en-US"));

        /// <summary>
        /// English - United Kingdom
        /// </summary>
        public static readonly LanguageCode ENG
            = new LanguageCode("ENG", "English_GB", CultureInfo.GetCultureInfo("en-GB"));

        /// <summary>
        /// French
        /// </summary>
        public static readonly LanguageCode FRA
            = new LanguageCode("FRA", "French", CultureInfo.GetCultureInfo("fr-FR"));

        /// <summary>
        /// German
        /// </summary>
        public static readonly LanguageCode DEU
            = new LanguageCode("DEU", "German", CultureInfo.GetCultureInfo("de-DE"));

        /// <summary>
        /// Italian
        /// </summary>
        public static readonly LanguageCode ITA
            = new LanguageCode("ITA", "Italian", CultureInfo.GetCultureInfo("it-IT"));

        /// <summary>
        /// Japanese
        /// </summary>
        public static readonly LanguageCode JPN
            = new LanguageCode("JPN", "Japanese", CultureInfo.GetCultureInfo("ja-JP"));

        /// <summary>
        /// Korean
        /// </summary>
        public static readonly LanguageCode KOR
            = new LanguageCode("KOR", "Korean", CultureInfo.GetCultureInfo("ko-KR"));

        /// <summary>
        /// Polish
        /// </summary>
        public static readonly LanguageCode PLK
            = new LanguageCode("PLK", "Polish", CultureInfo.GetCultureInfo("pl-PL"));

        /// <summary>
        /// Spanish
        /// </summary>
        public static readonly LanguageCode ESP
            = new LanguageCode("ESP", "Spanish", CultureInfo.GetCultureInfo("es"));

        /// <summary>
        /// Simplified Chinese
        /// </summary>
        public static readonly LanguageCode CHS
            = new LanguageCode("CHS", "Chinese_Simplified", CultureInfo.GetCultureInfo("zh-CN"));

        /// <summary>
        /// Traditional Chinese
        /// </summary>
        public static readonly LanguageCode CHT
            = new LanguageCode("CHT", "Chinese_Traditional", CultureInfo.GetCultureInfo("zh-Hant"));

        /// <summary>
        /// Brazilian Portuguese
        /// </summary>
        public static readonly LanguageCode PTB
            = new LanguageCode("PTB", "Brazilian_Portuguese", CultureInfo.GetCultureInfo("pt-BR"));

        /// <summary>
        /// Russian
        /// </summary>
        public static readonly LanguageCode RUS
            = new LanguageCode("RUS", "Russian", CultureInfo.GetCultureInfo("ru-RU"));

        /// <summary>
        /// Czech
        /// </summary>
        public static readonly LanguageCode CSY
            = new LanguageCode("CSY", "Czech", CultureInfo.GetCultureInfo("cs-CZ"));

        /// <summary>
        /// Hungarian
        /// </summary>
        public static readonly LanguageCode HUN
            = new LanguageCode("HUN", "Hungarian", CultureInfo.GetCultureInfo("hu-HU"));

        /// <summary>
        /// Unknown
        /// </summary>
        public static readonly LanguageCode Unknown
            = new LanguageCode("Unknown", "Unknown", CultureInfo.CurrentCulture);

        /// <summary>
        /// Returns language code by code or full code.
        /// </summary>
        /// <param name="languageCode">Language code.</param>
        /// <returns>Returns language code by code or full code.</returns>
        public static LanguageCode GetLanguageCode(string languageCode) {
            if(string.IsNullOrEmpty(languageCode)) {
                return Unknown;
            }

            // some revit libs generate RU instead of RUS in metadata
            if(languageCode.Equals("RU", StringComparison.CurrentCultureIgnoreCase)) {
                return RUS;
            }

            return GetLanguageCodes()
                       .FirstOrDefault(item =>
                           languageCode.Equals(item.Code, StringComparison.CurrentCultureIgnoreCase)
                           || languageCode.Equals(item.FullCode, StringComparison.CurrentCultureIgnoreCase))
                   ?? Unknown;
        }

        /// <summary>
        /// Returns language code by culture info.
        /// </summary>
        /// <param name="cultureInfo">Culture info.</param>
        /// <returns>Returns language code by culture info.</returns>
        public static LanguageCode GetLanguageCode(CultureInfo cultureInfo) {
            if(cultureInfo == null) {
                throw new ArgumentNullException(nameof(cultureInfo));
            }

            return GetLanguageCodes()
                       .FirstOrDefault(item => item.CultureInfo.Equals(cultureInfo))
                   ?? throw new NotSupportedException($"The {cultureInfo} is not supported.");
        }

        /// <summary>
        /// Creates language code object. 
        /// </summary>
        /// <param name="code">Language code value.</param>
        /// <param name="fullCode">Language full code value.</param>
        /// <param name="cultureInfo">Culture info.</param>
        private LanguageCode(string code, string fullCode, CultureInfo cultureInfo) {
            Code = code;
            FullCode = fullCode;
            CultureInfo = cultureInfo;
        }

        /// <summary>
        /// Language code value.
        /// </summary>
        public string Code { get; }

        /// <summary>
        /// Language full code value.
        /// </summary>
        public string FullCode { get; }

        /// <summary>
        /// Current culture info.
        /// </summary>
        public CultureInfo CultureInfo { get; }

        /// <summary>
        /// Display name.
        /// </summary>
        public string DisplayName => CultureInfo?.DisplayName;

        /// <summary>
        /// English name.
        /// </summary>
        public string EnglishName => CultureInfo.EnglishName;

        /// <summary>
        /// Returns all supported language codes.
        /// </summary>
        /// <returns>Returns all supported language codes.</returns>
        protected static IEnumerable<LanguageCode> GetLanguageCodes() {
            return typeof(LanguageCode)
                .GetFields(BindingFlags.Static | BindingFlags.Public)
                .Select(item => item.GetValue(null))
                .OfType<LanguageCode>();
        }

        /// <inheritdoc />
        public override string ToString() {
            return DisplayName;
        }

        #region IEquatable<LanguageCode>

        /// <inheritdoc />
        public bool Equals(LanguageCode other) {
            if(ReferenceEquals(null, other)) {
                return false;
            }

            if(ReferenceEquals(this, other)) {
                return true;
            }

            return string.Equals(Code, other.Code, StringComparison.CurrentCulture)
                   && string.Equals(FullCode, other.FullCode, StringComparison.CurrentCulture);
        }

        /// <inheritdoc />
        public override bool Equals(object obj) {
            if(ReferenceEquals(null, obj)) {
                return false;
            }

            if(ReferenceEquals(this, obj)) {
                return true;
            }

            if(obj.GetType() != this.GetType()) {
                return false;
            }

            return Equals((LanguageCode) obj);
        }

        /// <inheritdoc />
        public override int GetHashCode() {
            unchecked {
                return (StringComparer.CurrentCulture.GetHashCode(Code) * 397)
                       ^ StringComparer.CurrentCulture.GetHashCode(FullCode);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(LanguageCode left, LanguageCode right) {
            return Equals(left, right);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(LanguageCode left, LanguageCode right) {
            return !Equals(left, right);
        }

        #endregion
    }
}