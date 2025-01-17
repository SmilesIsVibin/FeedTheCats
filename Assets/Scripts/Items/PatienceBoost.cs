using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPatienceBoost", menuName = "Item/Patience Boost")]
public class PatienceBoost : Pickups
{
    [Header("PowerUp Details")]
    [SerializeField] public int scoreAmount;

    public override void Apply(GameObject target)
    {
        target.GetComponent<Player>().playerScore += scoreAmount;
        target.GetComponent<Player>().UpdateScore();
        target.GetComponent<Player>().AddPatienceBoost();
    }
}
