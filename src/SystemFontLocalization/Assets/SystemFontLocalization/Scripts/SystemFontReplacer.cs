using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace FontLocalization
{
    public static class SystemFontReplacer
    {
        private class FontData
        {
            public TMP_FontAsset BaseFontAsset;
            public string LanguageCode;
        }

        private static SystemFontNameList _fontNameAsset;
        private static List<FontData> _fonts = new List<FontData>();

        public static void SetFontNameAsset(SystemFontNameList fontNameAsset)
        {
            _fontNameAsset = fontNameAsset;
        }

        public static void AddFontAsset(TMP_FontAsset baseFontAsset)
        {
            if (_fonts.Exists(v => v.BaseFontAsset == baseFontAsset))
            {
                return;
            }

            _fonts.Add(new FontData
            {
                BaseFontAsset = baseFontAsset,
                LanguageCode = string.Empty
            });
        }

        public static void UpdateFallbackFont()
        {
            UpdateFallbackFontInternal(LocalizationSettings.SelectedLocale.Identifier.Code);
        }

        public static void ChangeFontLanguage(string languageCode)
        {
            UpdateFallbackFontInternal(languageCode);
        }

        private static void UpdateFallbackFontInternal(string languageCode)
        {
            if (_fontNameAsset == null || string.IsNullOrEmpty(languageCode))
            {
                return;
            }

            var shouldReplace = false;
            for (int i = 0; i < _fonts.Count; i++)
            {
                if (_fonts[i].LanguageCode != languageCode)
                {
                    shouldReplace = true;
                    break;
                }
            }

            if (!shouldReplace)
            {
                return;
            }

            if (!_fontNameAsset.TryGetFontNames(languageCode, out var languageFontNames))
            {
                Debug.Log($"Language font name is not found. {languageCode}");
                return;
            }

            if (languageFontNames == null || languageFontNames.Length == 0)
            {
                Debug.LogError($"Language font name is empty. {languageCode}");
                return;
            }

            var fallbackFontAsset = CreateFontAsset(languageFontNames);
            if (fallbackFontAsset == null)
            {
                Debug.LogError($"Failed to create fallbackfont. {languageCode}");
                return;
            }

            for (int i = 0; i < _fonts.Count; i++)
            {
                var fontData = _fonts[i];
                if (fontData.LanguageCode == languageCode)
                {
                    continue;
                }

                fontData.LanguageCode = languageCode;
                fontData.BaseFontAsset.fallbackFontAssetTable.Clear();
                fontData.BaseFontAsset.fallbackFontAssetTable.Add(fallbackFontAsset);
            }

            Debug.Log($"Fallback font replace completed. {languageCode}");
        }

        private static TMP_FontAsset CreateFontAsset(string[] searchFontNames)
        {
            var systemFontPaths = Font.GetPathsToOSFonts();
            var count = searchFontNames.Length;
            for (int i = 0; i < count; i++)
            {
                var languageFontName = searchFontNames[i];
                var systemFontPath = systemFontPaths.FirstOrDefault(v => v.Replace(" ", "").Contains(languageFontName.Replace(" ", ""), System.StringComparison.OrdinalIgnoreCase));
                if (string.IsNullOrEmpty(systemFontPath))
                {
                    Debug.Log($"Font not found. {languageFontName}");
                    continue;
                }

                var fallbackFontAsset = TMP_FontAsset.CreateFontAsset(
                    systemFontPath,
                    0,
                    90,
                    9,
                    UnityEngine.TextCore.LowLevel.GlyphRenderMode.SDFAA,
                    1024,
                    1024);

                if (fallbackFontAsset == null)
                {
                    Debug.LogWarning($"Failed to create fallbackfont. {languageFontName}");
                    continue;
                }

                fallbackFontAsset.atlasPopulationMode = AtlasPopulationMode.DynamicOS;
                return fallbackFontAsset;
            }

            Debug.LogError($"Failed to create fallbackfont.");
            return null;
        }
    }
}
