using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RunnerButton : MonoBehaviour
{
    public float destoryDelay = 2f;
    public UnityEvent onBeTouchedByRunner = new UnityEvent();
    private bool hasBeenTouched = false;

    public int score = 100;

    private void Awake()
    {
        transform.localScale = Vector3.one * Random.Range(0.3f, 0.5f);
    }

    private void HasBeenTouchedByRunner()
    {
        if (hasBeenTouched) return;

        hasBeenTouched = true;
        onBeTouchedByRunner.Invoke();
        Destroy(gameObject, destoryDelay);
        RunnerGameManager.Instance.ButtonTouched(score);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RunnerHand"))
            HasBeenTouchedByRunner();
    }
}
