using UnityEngine.SceneManagement;

namespace Scenes
{
    public class Loader
    {
        public static void Load(int sceneIndex)
        { 
            SceneManager.LoadScene(sceneIndex);
        }
    }
}