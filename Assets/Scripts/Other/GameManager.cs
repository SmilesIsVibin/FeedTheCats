using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Game Info")]
    [SerializeField] public float gameTimer;
    [SerializeField] public float patienceTimerReduction;
    [SerializeField] public float maxPatienceTimer;
    [SerializeField] public float minPatienceTimer;
    [SerializeField] public bool difficultyProgress;
    [SerializeField] public float currentTimer;
    [SerializeField] public float secondsToEvent;
    [SerializeField] public bool active;
    [SerializeField] public AudioSource musicSource;

    [Header("Game Screen")]
    [SerializeField] public GameObject currentGameScreen;

    [Header("Game Over Screen")]
    [SerializeField] public GameObject gameOverScreen;
    [SerializeField] public TextMeshProUGUI scoreTitle;
    [SerializeField] public TextMeshProUGUI scoreAcquired;
    [SerializeField] public TextMeshProUGUI coinsAcquired;
    [SerializeField] public TextMeshProUGUI gemsAcquired;

    [Header("Pause Screen")]
    [SerializeField] public GameObject pauseScreen;
    [SerializeField] public TextMeshProUGUI pauseScore;
    [SerializeField] public TextMeshProUGUI pauseCoins;
    [SerializeField] public TextMeshProUGUI pauseGems;

    [Header("Player Info")]
    [SerializeField] public Player player;
    [SerializeField] public SpriteRenderer skinRenderer;
    [SerializeField] public Sprite skin1;
    [SerializeField] public Sprite skin2;
    [SerializeField] public Sprite skin3;
    [SerializeField] public Sprite skin4;
    [SerializeField] public TextMeshProUGUI foodText;
    [SerializeField] public TextMeshProUGUI scoreText;
    [SerializeField] public TextMeshProUGUI coinText;
    [SerializeField] public TextMeshProUGUI gemText;

    [Header("Kitten 1")]
    [SerializeField] public KittenInformation activeKitten1;
    [SerializeField] public Slider kitten1Slider;

    [Header("Kitten 2")]
    [SerializeField] public KittenInformation activeKitten2;
    [SerializeField] public Slider kitten2Slider;

    [Header("Kitten 3")]
    [SerializeField] public KittenInformation activeKitten3;
    [SerializeField] public Slider kitten3Slider;

    [Header("Kitten 4")]
    [SerializeField] public KittenInformation activeKitten4;
    [SerializeField] public Slider kitten4Slider;

    private void Start()
    {
        active = true;

        activeKitten1.currentPatience = activeKitten1.maxPatience;
        activeKitten2.currentPatience = activeKitten2.maxPatience;
        activeKitten3.currentPatience = activeKitten3.maxPatience;
        activeKitten4.currentPatience = activeKitten4.maxPatience;

        kitten1Slider.maxValue = activeKitten1.maxPatience;
        kitten2Slider.maxValue = activeKitten2.maxPatience;
        kitten3Slider.maxValue = activeKitten3.maxPatience;
        kitten4Slider.maxValue = activeKitten4.maxPatience;

        gameOverScreen.SetActive(false);
        currentGameScreen.SetActive(true);
        pauseScreen.SetActive(false);

        UpdatePatience();
        UpdateFoodGathered();
        UpdateScoreDetail();

        skinRenderer = player.gameObject.GetComponent<SpriteRenderer>();

        SetupSkin(PlayerPrefs.GetInt("EquippedSkinData"));
    }

    private void Update()
    {
        if(active)
        {
            gameTimer += Time.deltaTime;
            if (gameTimer >= maxPatienceTimer)
            {
                ReducePatience();
                UpdatePatience();
                gameTimer = 0f;
            }
            if (difficultyProgress)
            {
                currentTimer += Time.deltaTime;
                if (currentTimer >= secondsToEvent)
                {
                    maxPatienceTimer -= patienceTimerReduction;
                    if (maxPatienceTimer <= minPatienceTimer)
                    {
                        difficultyProgress = false;
                    }
                    currentTimer = 0f;
                }
            }

            if (activeKitten1.currentPatience <= 0)
            {
                GameOver();
                active = false;
            }
            if (activeKitten2.currentPatience <= 0)
            {
                GameOver();
                active = false;
            }
            if (activeKitten3.currentPatience <= 0)
            {
                GameOver();
                active = false;
            }
            if (activeKitten4.currentPatience <= 0)
            {
                GameOver();
                active = false;
            }
        }
    }

    public void UpdatePatience()
    {
        kitten1Slider.value = activeKitten1.currentPatience;
        kitten2Slider.value = activeKitten2.currentPatience;
        kitten3Slider.value = activeKitten3.currentPatience;
        kitten4Slider.value = activeKitten4.currentPatience;
    }

    public void ReducePatience()
    {
        activeKitten1.currentPatience--;
        activeKitten2.currentPatience--;
        activeKitten3.currentPatience--;
        activeKitten4.currentPatience--;
    }

    public void UpdateFoodGathered()
    {
        foodText.text = player.foodGathered.ToString();
    }

    public void UpdateScoreDetail()
    {
        scoreText.text = player.playerScore.ToString();
    }

    public void UpdateCoinDetail()
    {
        coinText.text = player.playerCoinGathered.ToString();
    }

    public void UpdateGemDetail()
    {
        gemText.text = player.playerGemGathered.ToString();
    }

    public void AddPlayerScore(int score)
    {
        player.playerScore += score;
        UpdateScoreDetail();
    }

    public void GameOver()
    {
        musicSource.Pause();
        active = false;
        player.activePlayer = false;
        player.playerRB.linearVelocity *= 0f;
        player.movementJoystick.PointerUp();
        gameOverScreen.SetActive(true);
        currentGameScreen.SetActive(false);

        if (player.playerScore > PlayerPrefs.GetInt("PlayerScoreData"))
        {
            scoreText.text = "New High Score!";
            scoreAcquired.text = player.playerScore.ToString();
            PlayerPrefs.SetInt("PlayerScoreData", player.playerScore);
        }
        else
        {
            scoreText.text = "Your Score";
            scoreAcquired.text = player.playerScore.ToString();
        }

        coinsAcquired.text = player.playerCoinGathered.ToString();
        PlayerPrefs.SetInt("PlayerCoinData", PlayerPrefs.GetInt("PlayerCoinData") + player.playerCoinGathered);
        Debug.Log(PlayerPrefs.GetInt("PlayerCoinData"));
        gemsAcquired.text = player.playerGemGathered.ToString();
        PlayerPrefs.SetInt("PlayerGemData", PlayerPrefs.GetInt("PlayerGemData") + player.playerGemGathered);
        Debug.Log(PlayerPrefs.GetInt("PlayerGemData"));
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        musicSource.Pause();
        pauseScreen.SetActive(true);
        currentGameScreen.SetActive(false);
        pauseScore.text = player.playerScore.ToString();
        pauseCoins.text = player.playerCoinGathered.ToString();
        pauseGems.text = player.playerGemGathered.ToString();
    }

    public void UnPause()
    {
        Time.timeScale = 1f;
        musicSource.UnPause();
        pauseScreen.SetActive(false);
        currentGameScreen.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync("GameScene");
    }

    public void EndGame()
    {
        GameOver();
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync("MenuScene");
    }

    public void SetupSkin(int chosenSkin)
    {
        switch (chosenSkin)
        {
            case 1:
                skinRenderer.sprite = skin1;
                break;
            case 2:
                skinRenderer.sprite = skin2;
                break;
            case 3:
                skinRenderer.sprite = skin3;
                break;
            case 4:
                skinRenderer.sprite = skin4;
                break;
            default:
                skinRenderer.sprite = skin1;
                break;
        }
    }

    public void RestoreKittenPatience(float patienceNumber)
    {
        activeKitten1.PatienceIncrease(patienceNumber);
        activeKitten2.PatienceIncrease(patienceNumber);
        activeKitten3.PatienceIncrease(patienceNumber);
        activeKitten4.PatienceIncrease(patienceNumber);
    }
}
