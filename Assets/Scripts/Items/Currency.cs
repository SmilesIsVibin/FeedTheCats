using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFood", menuName = "Item/New Currency")]
public class Currency : Pickups
{
    [Header("PowerUp Details")]
    [SerializeField] public int currencyAmount;
    [SerializeField] public int scoreAmount;
    [SerializeField] private bool isCoin;
    [SerializeField] private bool isGem;

    public override void Apply(GameObject target)
    {
        if (isCoin)
        {
            target.GetComponent<Player>().playerCoinGathered += currencyAmount;
            target.GetComponent<Player>().UpdateCoins();
            target.GetComponent<Player>().playerScore += scoreAmount;
            target.GetComponent<Player>().UpdateScore();
        }
        else if (isGem)
        {
            target.GetComponent<Player>().playerGemGathered += currencyAmount;
            target.GetComponent<Player>().UpdateGems();
            target.GetComponent<Player>().playerScore += scoreAmount;
            target.GetComponent<Player>().UpdateScore();
        }
    }
}
