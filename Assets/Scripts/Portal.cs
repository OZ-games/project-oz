using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public static Portal portal { private set; get; }

    public GameObject portalStart;
    public GameObject portalEnd;

    private void Awake()
    {
        portal = this;
    }
}
