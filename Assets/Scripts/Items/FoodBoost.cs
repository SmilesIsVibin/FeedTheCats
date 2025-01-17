using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFoodBoost", menuName = "Item/Food Boost")]
public class FoodBoost : Pickups
{
    [Header("PowerUp Details")]
    [SerializeField] public int scoreAmount;

    public override void Apply(GameObject target)
    {
        target.GetComponent<Player>().playerScore += scoreAmount;
        target.GetComponent<Player>().UpdateScore();
        target.GetComponent<Player>().foodBoostActive = true;
        target.GetComponent<Player>().foodBoostTimer = target.GetComponent<Player>().foodBoostLimit;
    }
}
