using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RunnerButton : MonoBehaviour
{
    public float destoryDelay = 2f;
    public UnityEvent onBeTouchedByRunner = new UnityEvent();
    private bool hasBeenTouched = false;

    private void HasBeenTouchedByRunner()
    {
        if (hasBeenTouched) return;

        hasBeenTouched = true;
        onBeTouchedByRunner.Invoke();
        Destroy(gameObject, destoryDelay);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RunnerHand"))
            HasBeenTouchedByRunner();
    }
}
