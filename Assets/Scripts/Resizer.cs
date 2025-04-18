using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resizer : MonoBehaviour
{
    private Vector3 startSize;
    [SerializeField]
    private Vector3 endSize;
    [SerializeField]
    private float resizeTime;
    [SerializeField]
    private bool isLoop;
    [SerializeField]
    private AnimationCurve resizeAnim;

    private void OnEnable()
    {
        StartCoroutine(StartResize());
    }

    private IEnumerator StartResize()
    {
        startSize = transform.localScale;

        while (true)
        {
            yield return StartCoroutine(Resize(startSize, endSize, resizeTime));

            if (!isLoop)
            {
                break;
            }

            yield return StartCoroutine(Resize(endSize, startSize, resizeTime));
        }
    }

    private IEnumerator Resize(Vector3 start, Vector3 end, float time)
    {
        float current = 0;
        float percent = 0;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / time;

            //transform.localScale = Vector3.Lerp(start, end, percent);
            transform.localScale = Vector3.Lerp(start, end, resizeAnim.Evaluate(percent));

            yield return null;
        }
    }
}
