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

    public GameObject rewardPanel;
    public GameObject player;
    public TextMeshProUGUI rewardText;

    public GameObject goldTrophy;
    public GameObject silverTrophy;
    public GameObject bronzeTrophy;

    public int countdownTimer = 60;
    public int scoreCount;

    public int bronzeLimit = 75;
    public int silverLimit = 100;
    public int goldLimit = 125;

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
        if (rewardPanel != null)
            rewardPanel.SetActive(false);

        goldTrophy.SetActive(false);
        silverTrophy.SetActive(false);
        bronzeTrophy.SetActive(false);

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
            //StartCoroutine(RestartGame());
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
        Debug.Log("Final Score: " + scoreCount + " | Reward: " + reward);
        player.SetActive(false);

        if (rewardPanel != null)
        {
            rewardPanel.SetActive(true);

            if(rewardText != null)
            {
                rewardText.text = "Your Reward: " + reward;
            }
            else
            {
                Debug.LogWarning("Reward Text is not assigned in the inspector.");
            }
        }
    }

    private string GetReward()
    {
        if (scoreCount >= goldLimit)
        {
            goldTrophy.SetActive(true);
            return goldReward;
        }
        else if (scoreCount >= silverLimit)
        {
            silverTrophy.SetActive(true);
            return silverReward;
        }
        else if (scoreCount >= bronzeLimit)
        {
            bronzeTrophy.SetActive(true);
            return bronzeReward;
        }
        else
        {
            return "Sorry, No Reward";
        }
    }

    //IEnumerator RestartGame()
    //{
    //    yield return new WaitForSeconds(5f);
    //    SceneManager.LoadScene("Main Game");
    //}

    public void OnBackToGameClick()
    {
        SceneManager.LoadScene("Main Game");
    }

    public void OnBackToRegistClick()
    {
        SceneManager.LoadScene("Register");
    }
}
