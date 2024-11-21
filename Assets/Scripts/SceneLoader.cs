using System.Collections;
using MixedReality.Toolkit.Subsystems;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    private float loadingTime = 2.0f;

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
        StartCoroutine(LoadingScene(sceneName));
    }

    public void ReloadScene()
    {
        var currentScene = SceneManager.GetActiveScene();
        var index = currentScene.buildIndex;

        SceneManager.UnloadSceneAsync(currentScene);

        SceneManager.LoadScene(index, LoadSceneMode.Additive);
    }

    private IEnumerator LoadingScene(string sceneName)
    {
        Portal.portal.portalStart.SetActive(true);

        yield return new WaitForSeconds(loadingTime);

        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }
}
