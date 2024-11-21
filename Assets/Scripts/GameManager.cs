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

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        curHp = maxHp;

        audioSource = GetComponent<AudioSource>();
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
        sceneLoader.LoadScene("MainScene");
    }

    public void InitHpAndScore()
    {
        curHp = maxHp;
        slider.Value = (float)curHp / maxHp;
        hpText.text = "Hp   " + curHp;

        curScore = 0;
        scoreText.text = "Score " + curScore;

        missionFailedText.gameObject.SetActive(false);
    }
    
    public void UpdateHp(int value)
    {
        curHp += value;
        slider.Value = (float)curHp / maxHp;
        hpText.text = "Hp   " + curHp;

        if (curHp <= 0)
        {
            audioSource.PlayOneShot(gameOverVoice);
            GameOver();
        }
        else
        {
            audioSource.PlayOneShot(badVoice);
        }
    }

    public void UpdateScore(int value)
    {
        curScore += value;
        scoreText.text = "Score " + curScore;
        audioSource.PlayOneShot(goodVoice);
    }
}
