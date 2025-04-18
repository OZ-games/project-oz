using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public bool disableOnly;
    public float delay;

    void OnEnable()
    {
        if (disableOnly)
        {
            CancelInvoke();
            Invoke(nameof(Disable), delay);
        }
        else
        {
            Destroy(gameObject, delay);
        }
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }
}