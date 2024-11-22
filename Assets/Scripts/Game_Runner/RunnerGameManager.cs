using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class RunnerGameManager : MonoBehaviour
{
    public static RunnerGameManager Instance { private set; get; }

    private const string CLEAR_MESSAGE_FORMAT = "Game Clear!\nScore: {0}";

    public Timer timer;
    public float gameTime = 60;
    public TextMeshProUGUI gameClearText;

    public float gameStartDistance = 10f;
    public Transform headTransform;
    private Vector3 gameInitPosition;

    public UnityEvent onInit = new UnityEvent();
    public UnityEvent onStart = new UnityEvent();
    public UnityEvent onFinish = new UnityEvent();

    private bool hasInitialized = false;
    private bool hasStarted = false;
    public bool IsRunningGame => hasInitialized && hasStarted && !hasFinished;
    private bool hasFinished = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Reset()
    {
        hasInitialized = false;
        hasStarted = false;
        hasFinished = false;

        timer.Appear();
        timer.InitTimer(gameTime);
    }

    public void InitGame()
    {
        Reset();

        hasInitialized = true;
        gameInitPosition = headTransform.position;

        timer.OnFinishTimer.AddListener(FinishGame);
        onInit.Invoke();
    }

    public void StartGame()
    {
        if (!hasInitialized) return;
        if (hasStarted) return;

        hasStarted = true;
        timer.StartTimer();
        onStart.Invoke();
    }

    private void Update()
    {
        if (!hasInitialized || hasFinished) return;

        if (!hasStarted)
            UpdateWaitingGame();
    }

    private void UpdateWaitingGame()
    {
        if (Vector3.Distance(gameInitPosition, headTransform.position) >= gameStartDistance)
            StartGame();
    }

    private void FinishGame()
    {
        if (hasFinished) return;

        hasFinished = true;

        gameClearText.text = string.Format(CLEAR_MESSAGE_FORMAT, GameManager.instance.CurScore);

        onFinish.Invoke();

        Invoke(nameof(MoveToMainScene), 5f);
    }

    [Space]
    public SceneLoader sceneLoader;
    private void MoveToMainScene()
    {
        sceneLoader.LoadScene("MainScene");
    }

    public void ButtonTouched(int score)
    {
        GameManager.instance.UpdateScore(score);
    }
}
