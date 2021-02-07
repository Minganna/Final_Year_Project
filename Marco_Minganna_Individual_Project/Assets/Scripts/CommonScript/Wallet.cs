using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Core;

public class Wallet : MonoBehaviour,IpredicateEvaluator
{
    public TextMeshProUGUI wallet;
    int coinsHeld=0;


    public void addCoinsToWallet(int coins)
    {
        coinsHeld += coins;
        wallet.text = coinsHeld.ToString();
    }

    public bool? Evaluate(string predicate, string[] parameters)
    {
        switch(predicate)
        {
            case "HasEnoughGold":
                return coinsHeld > int.Parse(parameters[0]);
        }
        return null;
    }
}
