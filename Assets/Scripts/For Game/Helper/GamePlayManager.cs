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

    public int bronzeMin = 75;
    public int silverMin = 100;
    public int goldMin = 125;
    public int bronzeMax = 99;
    public int silverMax = 124;

    public string bronzeReward = "Bronze";
    public string silverReward = "Silver";  
    public string goldReward = "Gold";

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
            ShowReward();
            StartCoroutine(RestartGame());
        }
    }
    public void DisplayScore(int scoreValue)
    {
        if (scoreText == null)
            return;

        scoreCount += scoreValue;
        scoreText.text = "$ " + scoreCount;
    }

    private void ShowReward()
    {
        string reward = GetReward();
        Debug.Log("Final Score: " + scoreCount + "Reward: " + reward);
    }

    private string GetReward()
    {
        if (scoreCount >= bronzeMin && scoreCount <= bronzeMax)
        {
            return bronzeReward;
        }
        else if (scoreCount >= silverMin && scoreCount <= silverMax)
        {
            return silverReward;
        }
        else if (scoreCount >= goldMin)
        {
            return goldReward;
        }
        else
        {
            return "Sorry, No Reward";
        }
    }

    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Main Game");
    }
}
