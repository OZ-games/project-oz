using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    private Vector3 startPos;
    [SerializeField]
    private Vector3 endPos;
    [SerializeField]
    private float moveTime;
    [SerializeField]
    private bool isLoop;
    [SerializeField]
    private AnimationCurve moveAnim;

    private void OnEnable()
    {
        StartCoroutine(StartMove());
    }

    private IEnumerator StartMove()
    {
        startPos = transform.localPosition;

        while (true)
        {
            yield return StartCoroutine(Move(startPos, endPos, moveTime));

            if (!isLoop)
            {
                break;
            }

            yield return StartCoroutine(Move(endPos, startPos, moveTime));
        }
    }

    private IEnumerator Move(Vector3 start, Vector3 end, float time)
    {
        float current = 0;
        float percent = 0;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / time;

            //transform.localScale = Vector3.Lerp(start, end, percent);
            transform.localPosition = Vector3.Lerp(start, end, moveAnim.Evaluate(percent));

            yield return null;
        }
    }
}
