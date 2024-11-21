using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private static bool hasMainSceneLoaded = false;

    private void Awake()
    {
        LoadMainSceneOnApplicationStarted();
    }

    private void LoadMainSceneOnApplicationStarted()
    {
        if (hasMainSceneLoaded == false)
        {
            SceneManager.sceneLoaded += (scene, loadMode) =>
            {
                if (loadMode == LoadSceneMode.Additive)
                    SceneManager.SetActiveScene(scene);
            };

            SceneManager.LoadScene("MainScene", LoadSceneMode.Additive);

            hasMainSceneLoaded = true;
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    public void ReloadScene()
    {
        var currentScene = SceneManager.GetActiveScene();
        var index = currentScene.buildIndex;

        SceneManager.UnloadSceneAsync(currentScene);

        SceneManager.LoadScene(index, LoadSceneMode.Additive);
    }
}
