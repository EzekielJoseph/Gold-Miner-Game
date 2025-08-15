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
        if (rewardPanel != null)
            rewardPanel.SetActive(false);

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
