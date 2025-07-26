using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    private Timer timer;

    [SerializeField]
    private int maxHp = 100;

    private int curHp;
    private int curScore;

    public int CurHp => curHp;
    public int CurScore => curScore;

    [SerializeField]
    private TextMeshPro hpText;
    [SerializeField]
    private TextMeshPro scoreText;
    [SerializeField]
    private TextMeshProUGUI missionFailedText;
    [SerializeField]
    private MixedReality.Toolkit.UX.Slider slider;
    [SerializeField]
    private AudioClip goodVoice;
    [SerializeField]
    private AudioClip badVoice;
    [SerializeField]
    private AudioClip gameOverVoice;
    [SerializeField]
    private SceneLoader sceneLoader;

    private bool isGameOver;
    private AudioSource audioSource;

    public TextMeshProUGUI gameClearText;
    private const string GAMECLEAR_MESSAGE_FORMAT = "Game Clear!\nScore: {0}";
    float originalFontSize;


    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        curHp = maxHp;

        audioSource = GetComponent<AudioSource>();

        gameClearText.gameObject.SetActive(false);

        originalFontSize = scoreText.fontSize;
        SceneManager.sceneLoaded += RegisterGameStartEvents;
    }

    private void RegisterGameStartEvents(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MagicScene" || scene.name == "JengaScene")
        {
            timer.Appear();
            timer.InitTimer(120);
            timer.OnFinishTimer.AddListener(() =>
            {
                gameClearText.text = string.Format(GAMECLEAR_MESSAGE_FORMAT, CurScore);
                gameClearText.gameObject.SetActive(true);
                GameClear();
            });

            timer.StartTimer();
        }
    }

    private void GameClear()
    {
        if (!isGameOver)
        {
            isGameOver = true;
            StartCoroutine(BackToMainMenu());
        }
    }

    private void GameOver()
    {
        missionFailedText.gameObject.SetActive(true);
        if (!isGameOver)
        {
            isGameOver = true;
            StartCoroutine(BackToMainMenu());
        }
    }

    private IEnumerator BackToMainMenu()
    {
        yield return new WaitForSeconds(3f);

        isGameOver = false;
        InitHpAndScore();
        timer.Disappear();
        gameClearText.gameObject.SetActive(false);
        sceneLoader.LoadScene("MainScene");
    }

    public void InitHpAndScore()
    {
        curHp = maxHp;
        slider.Value = (float)curHp / maxHp;
        hpText.text = "Hp   " + curHp;

        curScore = 0;
        scoreText.text = "0000";

        missionFailedText.gameObject.SetActive(false);
    }

    public void UpdateHp(int value)
    {
        int prevHp = curHp;
        curHp += value;
        slider.Value = (float)curHp / maxHp;
        hpText.text = "Hp   " + curHp;

        if (prevHp > 0 && curHp <= 0)
        {
            audioSource.PlayOneShot(gameOverVoice);
            GameOver();
        }
        else if (curHp > 0)
        {
            audioSource.PlayOneShot(badVoice);
        }
    }

    Coroutine scoreCoroutine;
    public void UpdateScore(int value)
    {
        // scoreText.text = "Score " + curScore;

        if (scoreCoroutine != null)
        {
            StopCoroutine(scoreCoroutine);
            scoreCoroutine = null;
        }

        scoreCoroutine = StartCoroutine(UpdateScoreRoutine(curScore, curScore + value));

        curScore += value;
        audioSource.PlayOneShot(goodVoice);
    }

    [Header("Score")]
    [SerializeField]
    private AnimationCurve scoreScaleCurve;
    [SerializeField]
    private AnimationCurve scoreIncreaseCurve;
    [SerializeField]
    private float scoreScaleAnimTime;

    private IEnumerator UpdateScoreRoutine(int prevScore, int newScore)
    {
        float timer = 0;
        float progress = 0;

        while (progress < 1)
        {
            timer += Time.deltaTime;
            progress = timer / scoreScaleAnimTime;

            int displayedScore = (int)Mathf.Lerp(prevScore, newScore, scoreIncreaseCurve.Evaluate(progress));
            scoreText.text = string.Format("{0:D4}", displayedScore);

            scoreText.fontSize = Mathf.Lerp(originalFontSize, originalFontSize * 2, scoreScaleCurve.Evaluate(progress));

            yield return null;
        }

        scoreCoroutine = null;
    }
}
