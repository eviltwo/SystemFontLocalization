using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;

namespace FontLocalizationSample
{
    public class SampleComponent : MonoBehaviour
    {
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public void MoveLocale(int diff)
        {
            var locales = LocalizationSettings.AvailableLocales.Locales;
            var index = locales.IndexOf(LocalizationSettings.SelectedLocale);
            var nextIndex = (int)Mathf.Repeat(index + diff, locales.Count);
            LocalizationSettings.SelectedLocale = locales[nextIndex];
        }
    }
}
