using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSpeedBoost", menuName = "Item/Speed Boost")]
public class SpeedBoost : Pickups
{
    [Header("PowerUp Details")]
    [SerializeField] public int scoreAmount;

    public override void Apply(GameObject target)
    {
        target.GetComponent<Player>().playerScore += scoreAmount;
        target.GetComponent<Player>().UpdateScore();
        target.GetComponent<Player>().speedBoostActive = true;
        target.GetComponent<Player>().speedBoostTimer = target.GetComponent<Player>().speedBoostLimit;
    }
}
