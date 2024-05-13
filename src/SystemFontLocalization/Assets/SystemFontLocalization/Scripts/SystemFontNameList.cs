using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace FontLocalization
{
    [CreateAssetMenu(fileName = "SystemFontNameList", menuName = "SystemFontLocalization/SystemFontNameList")]
    public class SystemFontNameList : ScriptableObject
    {
        [Serializable]
        private class LocalizationFontSetting
        {
            [SerializeField]
            private string _languageCode = string.Empty;

            public string LanguageCode => _languageCode;

            [SerializeField]
            private string[] _fontNames = null;

            public string[] FontNames => _fontNames;
        }

        [SerializeField]
        private LocalizationFontSetting[] _settings = null;

        public void GetFontNames(string languageCode, List<string> results)
        {
            results.Clear();
            for (int i = 0; i < _settings.Length; i++)
            {
                var setting = _settings[i];
                if (Regex.IsMatch(languageCode, setting.LanguageCode))
                {
                    results.AddRange(setting.FontNames);
                }
            }
        }
    }
}
