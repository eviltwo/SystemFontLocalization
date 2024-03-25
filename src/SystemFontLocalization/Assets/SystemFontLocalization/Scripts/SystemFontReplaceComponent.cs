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
        }
    }
}
