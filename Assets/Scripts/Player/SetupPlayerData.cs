using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupPlayerData : MonoBehaviour
{

    [Header("Player Data")]
    [SerializeField] private int playerScoreData;
    [SerializeField] private int playerCoinData;
    [SerializeField] private int playerGemData;

    [Header("Player Skins")]
    [SerializeField] private int equippedSkinData;
    [SerializeField] private int skin1Unlocked;
    [SerializeField] private int skin2Unlocked;
    [SerializeField] private int skin3Unlocked;
    [SerializeField] private int skin4Unlocked;
    [SerializeField] private bool usingSkin1;
    [SerializeField] private bool usingSkin2;
    [SerializeField] private bool usingSkin3;
    [SerializeField] private bool usingSkin4;

    [Header("Player Powerups")]
    [SerializeField] private int speedBoostLevel;
    [SerializeField] private int patienceBoostLevel;
    [SerializeField] private int foodBoostLevel;

    [Header("Player Maps")]
    [SerializeField] private int equippedMapData;
    [SerializeField] private bool map1Unlocked;
    [SerializeField] private bool map2Unlocked;
    [SerializeField] private bool map3Unlocked;


    private void Start()
    {
        SetupInitialData();
        if (PlayerPrefs.HasKey("EquippedSkinData"))
        {
            if (PlayerPrefs.HasKey("Skin1Unlocked") && PlayerPrefs.GetInt("EquippedSkinData") == PlayerPrefs.GetInt("Skin1Unlocked"))
            {
                LoadSkinsData(PlayerPrefs.GetInt("EquippedSkinData"));
            }
            else if (PlayerPrefs.HasKey("Skin2Unlocked") && PlayerPrefs.GetInt("EquippedSkinData") == PlayerPrefs.GetInt("Skin2Unlocked"))
            {
                LoadSkinsData(PlayerPrefs.GetInt("EquippedSkinData"));
            }
            else if (PlayerPrefs.HasKey("Skin3Unlocked") && PlayerPrefs.GetInt("EquippedSkinData") == PlayerPrefs.GetInt("Skin3Unlocked"))
            {
                LoadSkinsData(PlayerPrefs.GetInt("EquippedSkinData"));
            }
            else if (PlayerPrefs.HasKey("Skin4Unlocked") && PlayerPrefs.GetInt("EquippedSkinData") == PlayerPrefs.GetInt("Skin4Unlocked"))
            {
                LoadSkinsData(PlayerPrefs.GetInt("EquippedSkinData"));
            }
            else {
                LoadSkinsData(PlayerPrefs.GetInt("EquippedSkinData"));
            }
        }
        else
        {
            PlayerPrefs.SetInt("EquippedSkinData", 1);
            PlayerPrefs.SetInt("Skin1Unlocked", 1);
            PlayerPrefs.SetInt("Skin2Unlocked", 0);
            PlayerPrefs.SetInt("Skin3Unlocked", 0);
            PlayerPrefs.SetInt("Skin4Unlocked", 0);
            LoadSkinsData(1);
        }

        skin1Unlocked = PlayerPrefs.GetInt("Skin1Unlocked");
        skin2Unlocked = PlayerPrefs.GetInt("Skin2Unlocked");
        skin3Unlocked = PlayerPrefs.GetInt("Skin3Unlocked");
        skin4Unlocked = PlayerPrefs.GetInt("Skin4Unlocked");
    }

    #region Setups
    private void SetupInitialData()
    {
        if (PlayerPrefs.HasKey("PlayerScoreData"))
        {
            LoadScoreData();
        }
        else
        {
            SetScoreData();
        }

        if (PlayerPrefs.HasKey("PlayerCoinData"))
        {
            LoadCoinData();
        }
        else
        {
            SetCoinData();
        }

        if (PlayerPrefs.HasKey("PlayerGemData"))
        {
            LoadGemData();
        }
        else
        {
            SetGemData();
        }

        if (PlayerPrefs.HasKey("SpeedBoostLevel"))
        {
            LoadSpeedBoostData();
        }
        else
        {
            SetSpeedBoostData();
        }

        if (PlayerPrefs.HasKey("PatienceBoostLevel"))
        {
            LoadPatienceBoostData();
        }
        else
        {
            SetPatienceBoostData();
        }

        if (PlayerPrefs.HasKey("FoodBoostLevel"))
        {
            LoadFoodBoostData();
        }
        else
        {
            SetFoodBoostData();
        }
    }
    private void SetScoreData()
    {
        PlayerPrefs.SetInt("PlayerScoreData", 0);
        LoadScoreData();
    }
    private void SetCoinData()
    {
        PlayerPrefs.SetInt("PlayerCoinData", 0);
        LoadCoinData();
    }
    private void SetGemData()
    {
        PlayerPrefs.SetInt("PlayerCoinData", 0);
        LoadGemData();
    }

    private void SetSpeedBoostData()
    {
        PlayerPrefs.SetInt("SpeedBoostLevel", 0);
        LoadSpeedBoostData();
    }
    private void SetPatienceBoostData()
    {
        PlayerPrefs.SetInt("PatienceBoostLevel", 0);
        LoadPatienceBoostData();
    }

    private void SetFoodBoostData()
    {
        PlayerPrefs.SetInt("FoodBoostLevel", 0);
        LoadFoodBoostData();
    }

    private void SetMapData()
    {

    }

    private void SetUpgradeData()
    {

    }
    #endregion

    #region Load
    public void LoadScoreData()
    {
        playerScoreData = PlayerPrefs.GetInt("PlayerScoreData");
    }
    public void LoadCoinData()
    {
        playerCoinData = PlayerPrefs.GetInt("PlayerCoinData");
    }
    public void LoadGemData()
    {
        playerGemData = PlayerPrefs.GetInt("PlayerGemData");
    }

    private void LoadSpeedBoostData()
    {
        speedBoostLevel = PlayerPrefs.GetInt("SpeedBoostLevel");
    }

    private void LoadPatienceBoostData()
    {
        patienceBoostLevel = PlayerPrefs.GetInt("PatienceBoostLevel");
    }

    private void LoadFoodBoostData()
    {
        foodBoostLevel = PlayerPrefs.GetInt("FoodBoostLevel");
    }

    public void LoadSkinsData(int skinChoice)
    {
        switch (skinChoice)
        {
            case 1:
                PlayerPrefs.SetInt("EquippedSkinData", skinChoice);
                equippedSkinData = 1;
                usingSkin1 = true;
                usingSkin2 = false;
                usingSkin3 = false;
                usingSkin4 = false;
                break;
            case 2:
                PlayerPrefs.SetInt("EquippedSkinData", skinChoice);
                equippedSkinData = 2;
                usingSkin1 = false;
                usingSkin2 = true;
                usingSkin3 = false;
                usingSkin4 = false;
                break;
            case 3:
                PlayerPrefs.SetInt("EquippedSkinData", skinChoice);
                equippedSkinData = 3;
                usingSkin1 = false;
                usingSkin2 = false;
                usingSkin3 = true;
                usingSkin4 = false;
                break;
            case 4:
                PlayerPrefs.SetInt("EquippedSkinData", skinChoice);
                equippedSkinData = 4;
                usingSkin1 = false;
                usingSkin2 = false;
                usingSkin3 = false;
                usingSkin4 = true;
                break;
            default:
                PlayerPrefs.SetInt("EquippedSkinData", 1);
                equippedSkinData = 1;
                usingSkin1 = true;
                usingSkin2 = false;
                usingSkin3 = false;
                usingSkin4 = false;
                break;
        }
    }

    /* private void LoadMapData()
    {

    } */
    #endregion
}
