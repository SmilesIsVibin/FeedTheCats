using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawners : MonoBehaviour
{
    [Header("Spawner Info")]
    [SerializeField] public List<GameObject> foodItemList;
    [SerializeField] public int maxItemSpawn;
    [SerializeField] public int itemToSpawn;
    [SerializeField] public float spawnRadius;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(this.transform.position, spawnRadius);
    }
}
