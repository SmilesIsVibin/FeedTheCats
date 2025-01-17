using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    [Header("Item Info")]
    [SerializeField] public Pickups itemType;
    [SerializeField] public bool isPowerUp;
    [SerializeField] public float destroyTimer;
    [SerializeField] public AudioSource audioSource;

    private void Start()
    {
        StartCoroutine(DestroyItem());
        if (isPowerUp)
        {
            audioSource = GameObject.Find("PowerUpAudioSource").GetComponent<AudioSource>();
        }
        else
        {
            audioSource = GameObject.Find("FoodAudioSource").GetComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            itemType.Apply(collision.gameObject);
            audioSource.Play();
            audioSource.pitch = Random.Range(0.9f, 1.1f);
            Destroy(this.gameObject);
        }
    }

    private IEnumerator DestroyItem()
    {
        yield return new WaitForSeconds(destroyTimer);
        Destroy(this.gameObject);
    }
}
