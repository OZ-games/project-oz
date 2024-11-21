using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    private float loadingTime = 2.0f;
    [SerializeField]
    private GameObject loadingObject;

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadingScene(sceneName));
    }

    private IEnumerator LoadingScene(string sceneName)
    {
        loadingObject.SetActive(true);

        yield return new WaitForSeconds(loadingTime);

        SceneManager.LoadScene(sceneName);
    }
}
