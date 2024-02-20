using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace FontLocalization
{
    public class SystemFontReplacer : MonoBehaviour
    {
        [SerializeField]
        private TMP_FontAsset _baseFontAsset = null;

        [SerializeField]
        private SystemFontNameList _fontNameAsset = null;

        private void OnEnable()
        {
            ReplaceLanguage(LocalizationSettings.SelectedLocale.Identifier.Code);
            LocalizationSettings.SelectedLocaleChanged += OnLocaleChanged;
        }

        private void OnDisable()
        {
            LocalizationSettings.SelectedLocaleChanged -= OnLocaleChanged;
        }

        private void OnLocaleChanged(Locale locale)
        {
            ReplaceLanguage(LocalizationSettings.SelectedLocale.Identifier.Code);
        }

        public void ReplaceLanguage(string languageCode)
        {
            Debug.Log($"Try replace font language. {languageCode}");
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

            _baseFontAsset.fallbackFontAssetTable.Clear();
            _baseFontAsset.fallbackFontAssetTable.Add(fallbackFontAsset);
            Debug.Log($"Fallback font replace completed. {languageCode}");
        }

        private static TMP_FontAsset CreateFontAsset(string[] searchFontNames)
        {
            var systemFontPaths = Font.GetPathsToOSFonts();
            var count = searchFontNames.Length;
            for (int i = 0; i < count; i++)
            {
                var languageFontName = searchFontNames[i];
                var systemFontPath = systemFontPaths.FirstOrDefault(v => v.Replace(" ", "").Contains(languageFontName.Replace(" ", ""), StringComparison.OrdinalIgnoreCase));
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
