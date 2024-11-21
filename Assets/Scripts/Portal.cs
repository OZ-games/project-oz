using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public static Portal portal { private set; get; }

    public GameObject portalStart;
    public GameObject portalEnd;

    private Vector3 startPos;
    private Vector3 endPScale;

    private void Awake()
    {
        portal = this;

        startPos = portalStart.transform.localPosition;
        endPScale = portalEnd.transform.localScale;
    }

    private void Start()
    {
        SceneManager.sceneLoaded += (scene, loadMode) =>
        {
            portalStart.transform.localPosition = startPos;
            portalEnd.transform.localScale = endPScale;
        };
    }
}
