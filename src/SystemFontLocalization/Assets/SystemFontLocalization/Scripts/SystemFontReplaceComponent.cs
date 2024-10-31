using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace FontLocalization
{
    public class SystemFontReplaceComponent : MonoBehaviour
    {
        [SerializeField]
        private TMP_FontAsset _baseFontAsset = null;

        [SerializeField]
        private SystemFontNameList _fontNameAsset = null;

        [SerializeField]
        private bool _rebuildCanvases = true;

        private void OnEnable()
        {
            SystemFontReplacer.SetFontNameAsset(_fontNameAsset);
            SystemFontReplacer.AddFontAsset(_baseFontAsset);
            SystemFontReplacer.UpdateFallbackFont();
            LocalizationSettings.SelectedLocaleChanged += OnLocaleChanged;
        }

        private void OnDisable()
        {
            LocalizationSettings.SelectedLocaleChanged -= OnLocaleChanged;
        }

        private void OnLocaleChanged(Locale locale)
        {
            SystemFontReplacer.ChangeFontLanguage(locale.Identifier.Code);
            if (_rebuildCanvases)
            {
                UpdateCanvases();
            }
        }

        private void UpdateCanvases()
        {
            var canvases = FindObjectsByType<Canvas>(FindObjectsSortMode.None);
            for (int i = 0; i < canvases.Length; i++)
            {
                var canvas = canvases[i];
                canvas.enabled = false;
                canvas.enabled = true;
            }
        }
    }
}
