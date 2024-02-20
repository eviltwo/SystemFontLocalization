using System;
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

        public bool TryGetFontNames(string languageCode, out string[] fontNames)
        {
            var count = _settings.Length;
            for (int i = 0; i < count; i++)
            {
                var setting = _settings[i];
                if (setting.LanguageCode == languageCode)
                {
                    fontNames = setting.FontNames;
                    return true;
                }
            }

            fontNames = new string[0];
            return false;
        }
    }
}
