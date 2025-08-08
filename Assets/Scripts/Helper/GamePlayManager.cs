using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI countdownText;

    public int countdownTimer = 60;
    private int scoreCount;

    [SerializeField]
    private UnityEngine.UI.Image scoreFillUI;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        DisplayScore(0);
        countdownText.text = countdownTimer.ToString();
        StartCoroutine("Countdown");
    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(1f);

        countdownTimer -= 1;
        countdownText.text = countdownTimer.ToString();

        StartCoroutine("Countdown");

        if (countdownTimer <= 0)
        {
            StopCoroutine("Countdown");
            StartCoroutine(RestartGame());
        }
    }
    public void DisplayScore(int scoreValue)
    {
        if (scoreText == null)
            return;

        scoreCount += scoreValue;
        scoreText.text = "$ " + scoreCount;

        scoreFillUI.fillAmount = (float)scoreCount / 100f;

        //if (scoreCount >= 100)
        //{
        //    StopCoroutine("Countdown");
        //    StartCoroutine(RestartGame());
        //}
    }

    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Main Game");
    }
}
