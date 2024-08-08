using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
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
        private static List<string> _fontNameBuffer = new List<string>();

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

            _fontNameAsset.GetFontNames(languageCode, _fontNameBuffer);
            if (_fontNameBuffer.Count == 0)
            {
                Debug.Log($"Language font name is not found. {languageCode}");
                return;
            }

            var fallbackFontAsset = CreateFontAsset(_fontNameBuffer);
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
                for (int j = 0; j < fontData.BaseFontAsset.fallbackFontAssetTable.Count; j++)
                {
                    var oldFallback = fontData.BaseFontAsset.fallbackFontAssetTable[j];
                    if (oldFallback != null)
                    {
                        Object.Destroy(oldFallback);
                    }
                }
                fontData.BaseFontAsset.fallbackFontAssetTable.Clear();
                fontData.BaseFontAsset.fallbackFontAssetTable.Add(fallbackFontAsset);
                Debug.Log($"Fallback font is replaced. {languageCode}");
            }
        }

        private static TMP_FontAsset CreateFontAsset(IReadOnlyList<string> searchFontNames)
        {
            var systemFontPaths = Font.GetPathsToOSFonts();
            for (int i = 0; i < searchFontNames.Count; i++)
            {
                var languageFontName = searchFontNames[i];
                var systemFontPath = FindFont(systemFontPaths, languageFontName);
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
                Debug.Log($"Fallback font is created. Source:{systemFontPath}");
                return fallbackFontAsset;
            }

            Debug.LogError($"Failed to create fallbackfont.");
            return null;
        }

        private static string FindFont(string[] fontPaths, string pattern)
        {
            for (int i = 0; i < fontPaths.Length; i++)
            {
                var fontPath = fontPaths[i];
                var fileName = Path.GetFileNameWithoutExtension(fontPath);
                if (Regex.IsMatch(fileName, pattern))
                {
                    return fontPath;
                }
            }

            return string.Empty;
        }
    }
}
