using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneLoadEventListener : MonoBehaviour
{
    [SerializeField]
    private List<SceneLoadEventInfo> sceneLoadEventInfos;

    private void Start()
    {
        SceneManager.sceneLoaded += (scene, mode) =>
        {
            var info = sceneLoadEventInfos.Find(info => info.targetSceneName == scene.name);
            info?.onLoad.Invoke();
        };

        SceneManager.sceneUnloaded += (scene) =>
        {
            var info = sceneLoadEventInfos.Find(info => info.targetSceneName == scene.name);
            info?.onUnload.Invoke();
        };
    }
}

[Serializable]
public class SceneLoadEventInfo
{
    public string targetSceneName;
    public UnityEvent onLoad;
    public UnityEvent onUnload;
}
