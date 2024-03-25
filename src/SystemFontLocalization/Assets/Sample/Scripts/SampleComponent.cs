using UnityEngine;
using UnityEngine.SceneManagement;

namespace FontLocalizationSample
{
    public class SampleComponent : MonoBehaviour
    {
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
