using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("Menus")]
    [SerializeField] private GameObject mainScreen;
    [SerializeField] private GameObject tutorialScreen;
    [SerializeField] private GameObject optionsScreen;
    [SerializeField] private GameObject deleteDataPanel;
    [SerializeField] private Animator canvasAnimator;

    [Header("Misc")]
    [SerializeField] private SetupPlayerData playerData;

    #region Skin Shop Ui
    [Header("Skin 1")]
    [SerializeField] private bool isSkin1Equipped;
    [SerializeField] private Button equipButton1;
    [SerializeField] private TextMeshProUGUI equipButton1text;

    [Header("Skin 2")]
    [SerializeField] private TextMeshProUGUI skin2Status;
    [SerializeField] private TextMeshProUGUI skin2Price;
    [SerializeField] private bool isSkin2Equipped;
    [SerializeField] private Button equipButton2;
    [SerializeField] private TextMeshProUGUI equipButton2text;
    [SerializeField] private Button buyButton2;

    [Header("Skin 3")]
    [SerializeField] private TextMeshProUGUI skin3Status;
    [SerializeField] private TextMeshProUGUI skin3Price;
    [SerializeField] private bool isSkin3Equipped;
    [SerializeField] private Button equipButton3;
    [SerializeField] private TextMeshProUGUI equipButton3text;
    [SerializeField] private Button buyButton3;

    [Header("Skin 4")]
    [SerializeField] private TextMeshProUGUI skin4Status;
    [SerializeField] private TextMeshProUGUI skin4Price;
    [SerializeField] private bool isSkin4Equipped;
    [SerializeField] private Button equipButton4;
    [SerializeField] private TextMeshProUGUI equipButton4text;
    [SerializeField] private Button buyButton4;

    [Header("Player Gems")]
    [SerializeField] private TextMeshProUGUI gemText;
    #endregion

    #region Powerups Shop Ui
    [Header("Powerup 1")]
    [SerializeField] private Button upgradeButton1;
    [SerializeField] private TextMeshProUGUI powerup1PriceText;
    [SerializeField] private TextMeshProUGUI powerup1LevelText;
    [SerializeField] private int powerup1MaxLevel;
    [SerializeField] private List<int> powerup1Prices;

    [Header("Powerup 2")]
    [SerializeField] private Button upgradeButton2;
    [SerializeField] private TextMeshProUGUI powerup2PriceText;
    [SerializeField] private TextMeshProUGUI powerup2LevelText;
    [SerializeField] private int powerup2MaxLevel;
    [SerializeField] private List<int> powerup2Prices;

    [Header("Powerup 2")]
    [SerializeField] private Button upgradeButton3;
    [SerializeField] private TextMeshProUGUI powerup3PriceText;
    [SerializeField] private TextMeshProUGUI powerup3LevelText;
    [SerializeField] private int powerup3MaxLevel;
    [SerializeField] private List<int> powerup3Prices;

    [Header("Player Gems")]
    [SerializeField] private TextMeshProUGUI coinText;
    #endregion

    #region Main Functions
    void Awake()
    {
        //Screen.SetResolution(360, 640, false);
        Screen.SetResolution(540, 960, false);
    }

    private void Start()
    {
        mainScreen.SetActive(true);
        tutorialScreen.SetActive(false);
        optionsScreen.SetActive(false);
        deleteDataPanel.SetActive(false);
        canvasAnimator.Play("StartUp_Animation");
        powerup1MaxLevel = PlayerPrefs.GetInt("SpeedBoostLevel") + 1;
        powerup2MaxLevel = PlayerPrefs.GetInt("PatienceBoostLevel") + 1;
        powerup3MaxLevel = PlayerPrefs.GetInt("FoodBoostLevel") + 1;
        SetupSkinsShop();
        SetupPowerupShop();
    }

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("GameScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OpenTutorial()
    {
        canvasAnimator.SetTrigger("HowToPlay");
    }

    public void CloseTutorial()
    {
        canvasAnimator.SetTrigger("CloseTutorial");
    }

    public void OpenOptions()
    {
        canvasAnimator.SetTrigger("OpenSettings");
    }

    public void CloseOptions()
    {
        canvasAnimator.SetTrigger("CloseSettings");
    }
    #endregion

    #region Shop Functions
    public void OpenShop()
    {
        canvasAnimator.SetTrigger("OpenShop");
    }

    public void CloseShop()
    {
        canvasAnimator.SetTrigger("CloseShop");
    }

    public void GoToSkinShop()
    {
        canvasAnimator.SetTrigger("GoToSkins");
    }

    public void CloseSkinShop()
    {
        canvasAnimator.SetTrigger("CloseSkinShop");
    }

    public void GoToPowerupShop()
    {
        canvasAnimator.SetTrigger("GoToPowerups");
    }

    public void ClosePowerupShop()
    {
        canvasAnimator.SetTrigger("ClosePowerUpShop");
    }
    #endregion

    #region Skin Shop Functions
    public void SetupSkinsShop()
    {
        if (PlayerPrefs.HasKey("EquippedSkinData"))
        {
            if (PlayerPrefs.GetInt("Skin2Unlocked") == 2)
            {
                skin2Status.text = "Owned";
                skin2Price.gameObject.SetActive(false);
                buyButton2.gameObject.SetActive(false);
                equipButton2.gameObject.SetActive(true);
            }
            if (PlayerPrefs.GetInt("Skin3Unlocked") == 3)
            {
                skin3Status.text = "Owned";
                skin3Price.gameObject.SetActive(false);
                buyButton3.gameObject.SetActive(false);
                equipButton3.gameObject.SetActive(true);
            }
            if (PlayerPrefs.GetInt("Skin4Unlocked") == 4)
            {
                skin4Status.text = "Owned";
                skin4Price.gameObject.SetActive(false);
                buyButton4.gameObject.SetActive(false);
                equipButton4.gameObject.SetActive(true);
            }
            UpdateEquippedSkin(PlayerPrefs.GetInt("EquippedSkinData"));
        }
        else
        {
            PlayerPrefs.SetInt("EquippedSkinData", 1);
            UpdateEquippedSkin(1);
        }
        gemText.text = PlayerPrefs.GetInt("PlayerGemData").ToString() + " Gems";
    }

    public void BuySkin2(int skinPrice)
    {
        if(PlayerPrefs.GetInt("PlayerGemData") >= skinPrice)
        {
            PlayerPrefs.SetInt("PlayerGemData", PlayerPrefs.GetInt("PlayerGemData") - skinPrice);
            PlayerPrefs.SetInt("Skin2Unlocked", 2);
            UpdateEquippedSkin(2);
            SetupSkinsShop();
        }
        else
        {
            Debug.Log("Insufficient Gems");
        }
    }

    public void BuySkin3(int skinPrice)
    {
        if (PlayerPrefs.GetInt("PlayerGemData") >= skinPrice)
        {
            PlayerPrefs.SetInt("PlayerGemData", PlayerPrefs.GetInt("PlayerGemData") - skinPrice);
            PlayerPrefs.SetInt("Skin3Unlocked", 3);
            UpdateEquippedSkin(3);
            SetupSkinsShop();
        }
        else
        {
            Debug.Log("Insufficient Gems");
        }
    }

    public void BuySkin4(int skinPrice)
    {
        if (PlayerPrefs.GetInt("PlayerGemData") >= skinPrice)
        {
            PlayerPrefs.SetInt("PlayerGemData", PlayerPrefs.GetInt("PlayerGemData") - skinPrice);
            PlayerPrefs.SetInt("Skin4Unlocked", 4);
            UpdateEquippedSkin(4);
            SetupSkinsShop();
        }
        else
        {
            Debug.Log("Insufficient Gems");
        }
    }

    public void UpdateSkin(int numHolder)
    {
        UpdateEquippedSkin(numHolder);
    }

    public void UpdateEquippedSkin(int chosenSkin)
    {
        switch (chosenSkin)
        {
            case 1:
                isSkin1Equipped = true;
                equipButton1.interactable = false;
                equipButton1text.text = "EQUIPPED";

                isSkin2Equipped = false;
                equipButton2.interactable = true;
                equipButton2text.text = "EQUIP";

                isSkin3Equipped = false;
                equipButton3.interactable = true;
                equipButton3text.text = "EQUIP";

                isSkin4Equipped = false;
                equipButton4.interactable = true;
                equipButton4text.text = "EQUIP";
                break;
            case 2:
                isSkin1Equipped = false;
                equipButton1.interactable = true;
                equipButton1text.text = "EQUIP";

                isSkin2Equipped = true;
                equipButton2.interactable = false;
                equipButton2text.text = "EQUIPPED";

                isSkin3Equipped = false;
                equipButton3.interactable = true;
                equipButton3text.text = "EQUIP";

                isSkin4Equipped = false;
                equipButton4.interactable = true;
                equipButton4text.text = "EQUIP";
                break;
            case 3:
                isSkin1Equipped = false;
                equipButton1.interactable = true;
                equipButton1text.text = "EQUIP";

                isSkin2Equipped = false;
                equipButton2.interactable = true;
                equipButton2text.text = "EQUIP";

                isSkin3Equipped = true;
                equipButton3.interactable = false;
                equipButton3text.text = "EQUIPPED";

                isSkin4Equipped = false;
                equipButton4.interactable = true;
                equipButton4text.text = "EQUIP";
                break;
            case 4:
                isSkin1Equipped = false;
                equipButton1.interactable = true;
                equipButton1text.text = "EQUIP";

                isSkin2Equipped = false;
                equipButton2.interactable = true;
                equipButton2text.text = "EQUIP";

                isSkin3Equipped = false;
                equipButton3.interactable = true;
                equipButton3text.text = "EQUIP";

                isSkin4Equipped = true;
                equipButton4.interactable = false;
                equipButton4text.text = "EQUIPPED";
                break;
            default:
                isSkin1Equipped = true;
                equipButton1.interactable = false;
                equipButton1text.text = "EQUIPPED";

                isSkin2Equipped = false;
                equipButton2.interactable = true;
                equipButton2text.text = "EQUIP";

                isSkin3Equipped = false;
                equipButton3.interactable = true;
                equipButton3text.text = "EQUIP";

                isSkin4Equipped = false;
                equipButton4.interactable = true;
                equipButton4text.text = "EQUIP";
                break;
        }
        playerData.LoadSkinsData(chosenSkin);
    }
    #endregion

    #region Powerup Shop Functions
    public void SetupPowerupShop()
    {
        if(PlayerPrefs.GetInt("SpeedBoostLevel") >= powerup1Prices.Count)
        {
            powerup1LevelText.text = "Level " + powerup1MaxLevel.ToString();
            powerup1PriceText.text = "MAX LEVEL";
            upgradeButton1.gameObject.SetActive(false);
        }
        else
        {
            powerup1LevelText.text = "Level " + powerup1MaxLevel.ToString();
            powerup1PriceText.text = powerup1Prices[PlayerPrefs.GetInt("SpeedBoostLevel")].ToString() + " Coins";
            upgradeButton1.gameObject.SetActive(true);
        }

        if (PlayerPrefs.GetInt("PatienceBoostLevel") >= powerup2Prices.Count)
        {
            powerup2LevelText.text = "Level " + powerup2MaxLevel.ToString();
            powerup2PriceText.text = "MAX LEVEL";
            upgradeButton2.gameObject.SetActive(false);
        }
        else
        {
            powerup2LevelText.text = "Level " + powerup2MaxLevel.ToString();
            powerup2PriceText.text = powerup2Prices[PlayerPrefs.GetInt("PatienceBoostLevel")].ToString() + " Coins";
            upgradeButton2.gameObject.SetActive(true);
        }

        if (PlayerPrefs.GetInt("FoodBoostLevel") >= powerup1Prices.Count)
        {
            powerup3LevelText.text = "Level " + powerup3MaxLevel.ToString();
            powerup3PriceText.text = "MAX LEVEL";
            upgradeButton3.gameObject.SetActive(false);
        }
        else
        {
            powerup3LevelText.text = "Level " + powerup3MaxLevel.ToString();
            powerup3PriceText.text = powerup3Prices[PlayerPrefs.GetInt("FoodBoostLevel")].ToString() + " Coins";
            upgradeButton3.gameObject.SetActive(true);
        }

        coinText.text = PlayerPrefs.GetInt("PlayerCoinData").ToString() + " Coins";
    }

    public void UpgradePowerup1()
    {
        if (PlayerPrefs.GetInt("PlayerCoinData") >= powerup1Prices[PlayerPrefs.GetInt("SpeedBoostLevel")])
        {
            PlayerPrefs.SetInt("PlayerCoinData", PlayerPrefs.GetInt("PlayerCoinData") - powerup1Prices[PlayerPrefs.GetInt("SpeedBoostLevel")]);
            PlayerPrefs.SetInt("SpeedBoostLevel", PlayerPrefs.GetInt("SpeedBoostLevel") + 1);
            powerup1MaxLevel = PlayerPrefs.GetInt("SpeedBoostLevel") + 1;
            SetupPowerupShop();
        }
        else
        {
            Debug.Log("Insufficient Coins");
        }
    }

    public void UpgradePowerup2()
    {
        if (PlayerPrefs.GetInt("PlayerCoinData") >= powerup2Prices[PlayerPrefs.GetInt("PatienceBoostLevel")])
        {
            PlayerPrefs.SetInt("PlayerCoinData", PlayerPrefs.GetInt("PlayerCoinData") - powerup1Prices[PlayerPrefs.GetInt("SpeedBoostLevel")]);
            PlayerPrefs.SetInt("PatienceBoostLevel", PlayerPrefs.GetInt("PatienceBoostLevel") + 1);
            powerup2MaxLevel = PlayerPrefs.GetInt("PatienceBoostLevel") + 1;
            SetupPowerupShop();
        }
        else
        {
            Debug.Log("Insufficient Coins");
        }
    }

    public void UpgradePowerup3()
    {
        if (PlayerPrefs.GetInt("PlayerCoinData") >= powerup3Prices[PlayerPrefs.GetInt("FoodBoostLevel")])
        {
            PlayerPrefs.SetInt("PlayerCoinData", PlayerPrefs.GetInt("PlayerCoinData") - powerup1Prices[PlayerPrefs.GetInt("FoodBoostLevel")]);
            PlayerPrefs.SetInt("FoodBoostLevel", PlayerPrefs.GetInt("FoodBoostLevel") + 1);
            powerup3MaxLevel = PlayerPrefs.GetInt("FoodBoostLevel") + 1;
            SetupPowerupShop();
        }
        else
        {
            Debug.Log("Insufficient Coins");
        }
    }
    #endregion

    #region Player Data Function
    public void OpenDeleteDataPanel()
    {
        deleteDataPanel.SetActive(true);
    }
    public void CloseDeleteDataPanel()
    {
        deleteDataPanel.SetActive(false);
    }

    public void DeletePlayerData()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadSceneAsync("MenuScene");
    }
    #endregion
}
