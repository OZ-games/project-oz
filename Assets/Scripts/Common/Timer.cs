using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public UnityEvent OnStartTimer { private set; get; } = new UnityEvent();
    public UnityEvent OnFinishTimer { private set; get; } = new UnityEvent();

    private float time;
    private float timer;

    public bool IsRunning { private set; get; } = false;

    public string tiemrFormat = "{0:D2}";
    public TextMeshPro timerText;

    public void InitTimer(float time)
    {
        this.time = time;
        this.timer = time;

        IsRunning = false;
        OnStartTimer.RemoveAllListeners();
        OnFinishTimer.RemoveAllListeners();

        UpdateUI();
    }

    public void ResetTimer()
    {
        InitTimer(time);
    }

    public void StartTimer()
    {
        IsRunning = true;
        OnStartTimer.Invoke();
    }

    private void Update()
    {
        if (!IsRunning) return;

        timer -= Time.deltaTime;
        UpdateUI();

        if (timer <= 0)
            FinishTimer();
    }

    public void FinishTimer()
    {
        if (!IsRunning) return;

        IsRunning = false;

        OnFinishTimer.Invoke();
    }

    public void Appear()
    {
        this.gameObject.SetActive(true);
    }

    public void Disappear()
    {
        this.gameObject.SetActive(false);
    }

    private void UpdateUI()
    {
        timerText.text = string.Format(tiemrFormat, (int)timer);
    }
}
