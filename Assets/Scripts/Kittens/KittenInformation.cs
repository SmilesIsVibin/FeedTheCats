using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KittenInformation : MonoBehaviour
{
    [Header("Kitten Information")]
    [SerializeField] public float maxPatience;
    [SerializeField] public float minPatience;
    [SerializeField] public float currentPatience;
    [SerializeField] public float addedPatience;
    [SerializeField] public int addedScore;
    [SerializeField] public int fishRequirement;
    [SerializeField] public GameManager gameManager;
    [SerializeField] public bool active;
    [SerializeField] public Player player;
    [SerializeField] public AudioSource kittenAudioSource;
    [SerializeField] public GameObject feedButton;

    private void Start()
    {
        active = false;
        minPatience = maxPatience - 2f;
        feedButton.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && active && player.foodGathered >= fishRequirement)
        {
            FeedKitten();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && currentPatience <= minPatience)
        {
            active = true;
            feedButton.SetActive(true);
        }
    }

    private void OnTriggerExit2D()
    {
        active = false;
        feedButton.SetActive(false);
    }

    public void FeedKitten()
    {
        if(active && player.foodGathered >= fishRequirement)
        {
            PatienceIncrease(addedPatience);
            gameManager.AddPlayerScore(addedScore);
            player.foodGathered -= fishRequirement;
            kittenAudioSource.Play();
            kittenAudioSource.pitch = Random.Range(0.95f, 1.05f);
            player.UpdateInventory();
        }
        else
        {
            Debug.Log("Can't Feed");
        }
    }

    public void PatienceIncrease(float num)
    {
        currentPatience += num;
        if (currentPatience > maxPatience)
        {
            currentPatience = maxPatience;
        }
        gameManager.UpdatePatience();
    }
}
