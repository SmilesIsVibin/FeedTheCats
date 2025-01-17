using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Assign Elements
    [Header("Movement Information")]
    [SerializeField] public float movementSpeed;
    [SerializeField] public Rigidbody2D playerRB;
    [SerializeField] public bool facingRight;
    [SerializeField] public bool activePlayer;
    private Vector2 movementInput;

    [Header("Speed Boost Info")]
    [SerializeField] public float speedBoost;
    [SerializeField] public float speedBoostlevel;
    [SerializeField] public float speedBoostTimer;
    [SerializeField] public float speedBoostLimit;
    [SerializeField] public bool speedBoostActive;

    [Header("Inventory Details")]
    [SerializeField] public int foodGathered;
    [SerializeField] public int playerScore;
    [SerializeField] public int playerCoinGathered;
    [SerializeField] public int playerGemGathered;

    [Header("Food Boost Info")]
    [SerializeField] public int foodBoost;
    [SerializeField] public float foodBoostLevel;
    [SerializeField] public float foodBoostTimer;
    [SerializeField] public float foodBoostLimit;
    [SerializeField] public bool foodBoostActive;

    [Header("Misc")]
    [SerializeField] public GameManager gameManager;
    [SerializeField] public float patienceBoostLevel;
    [SerializeField] public float patienceIncrease;

    [Header("Joystick Info")]
    [SerializeField] public MovementJoystick movementJoystick;
    #endregion

    private void Start()
    {
        foodGathered = 0;
        playerScore = 0;
        speedBoostActive = false;
        activePlayer = true;

        speedBoostlevel = PlayerPrefs.GetInt("SpeedBoostLevel");
        switch (speedBoostlevel)
        {
            case 0:
                speedBoostLimit = 5f;
                break;
            case 1:
                speedBoostLimit = 7.5f;
                break;
            case 2:
                speedBoostLimit = 10f;
                break;
            case 3:
                speedBoostLimit = 12.5f;
                break;
            case 4:
                speedBoostLimit = 15f;
                break;
            default:
                speedBoostLimit = 5f;
                break;
        }

        foodBoostLevel = PlayerPrefs.GetInt("FoodBoostLevel");
        switch (foodBoostLevel)
        {
            case 0:
                foodBoost = 2;
                break;
            case 1:
                foodBoost = 3;
                break;
            case 2:
                foodBoost = 4;
                break;
            case 3:
                foodBoost = 5;
                break;
            case 4:
                foodBoost = 6;
                break;
            default:
                foodBoost = 2;
                break;
        }

        patienceBoostLevel = PlayerPrefs.GetInt("PatienceBoostLevel");
        switch (patienceBoostLevel)
        {
            case 0:
                patienceIncrease = 2f;
                break;
            case 1:
                patienceIncrease = 4f;
                break;
            case 2:
                patienceIncrease = 6f;
                break;
            case 3:
                patienceIncrease = 8f;
                break;
            case 4:
                patienceIncrease = 10f;
                break;
            default:
                break;
        }
    }

    #region Movement System
    private void Update()
    {
        if(movementJoystick.joystickActive == false)
        {
            if (activePlayer && Time.timeScale != 0)
            {
                movementInput.x = Input.GetAxisRaw("Horizontal");
                movementInput.y = Input.GetAxisRaw("Vertical");

                movementInput.Normalize();

                if (speedBoostActive)
                {
                    speedBoostTimer -= Time.deltaTime;
                    playerRB.linearVelocity = movementInput * (movementSpeed * speedBoost);
                    if (speedBoostTimer <= 0)
                    {
                        speedBoostActive = false;
                    }
                }
                else
                {
                    playerRB.linearVelocity = movementInput * movementSpeed;
                }

                if (movementInput.x > 0 && !facingRight)
                {
                    FlipCharacter();
                }
                else if (movementInput.x < 0 && facingRight)
                {
                    FlipCharacter();
                }
            }
        }

        if (foodBoostActive)
        {
            foodBoostTimer -= Time.deltaTime;
            if(foodBoostTimer <= 0)
            {
                foodBoostActive = false;
            }
        }
    }

    void FixedUpdate()
    {
        if (movementJoystick.joystickActive)
        {
            if (movementJoystick.joystickVec.y != 0)
            {
                if (speedBoostActive)
                {
                    speedBoostTimer -= Time.deltaTime;
                    playerRB.linearVelocity = new Vector2(movementJoystick.joystickVec.x * movementSpeed * speedBoost, movementJoystick.joystickVec.y * movementSpeed * speedBoost);
                    if (speedBoostTimer <= 0)
                    {
                        speedBoostActive = false;
                    }
                }
                else
                {
                    playerRB.linearVelocity = new Vector2(movementJoystick.joystickVec.x * movementSpeed, movementJoystick.joystickVec.y * movementSpeed);
                }

                if (movementJoystick.joystickVec.x > 0 && !facingRight)
                {
                    FlipCharacter();
                }
                else if (movementJoystick.joystickVec.x < 0 && facingRight)
                {
                    FlipCharacter();
                }
            }
            else
            {
                playerRB.linearVelocity = Vector2.zero;
            }
        }
    }

    private void FlipCharacter()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
    #endregion

    public void UpdateInventory()
    {
        gameManager.UpdateFoodGathered();
    }

    public void UpdateScore()
    {
        gameManager.UpdateScoreDetail();
    }

    public void UpdateCoins()
    {
        gameManager.UpdateCoinDetail();
    }

    public void UpdateGems()
    {
        gameManager.UpdateGemDetail();
    }

    public void AddFood(int number)
    {
        if (foodBoostActive)
        {
            foodGathered += number * foodBoost;
            UpdateInventory();
        }
        else
        {
            foodGathered += number;
            UpdateInventory();
        }
    }

    public void AddPatienceBoost()
    {
        gameManager.RestoreKittenPatience(patienceIncrease);
    }
}
