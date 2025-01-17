using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFood", menuName = "Item/Food")]
public class Food : Pickups
{
    [Header("Food Details")]
    [SerializeField] public int amount;
    [SerializeField] public int scoreAmount;

    public override void Apply(GameObject target)
    {
        target.GetComponent<Player>().AddFood(amount);
        target.GetComponent<Player>().playerScore += scoreAmount;
        target.GetComponent<Player>().UpdateScore();
    }
}
