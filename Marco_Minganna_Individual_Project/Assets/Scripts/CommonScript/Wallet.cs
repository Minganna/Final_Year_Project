using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Core;

public class Wallet : MonoBehaviour,IpredicateEvaluator
{
    public TextMeshProUGUI wallet;
    static int coinsHeld=0;

    private void Start()
    {
        wallet.text = coinsHeld.ToString();
    }
    public void addCoinsToWallet(int coins)
    {
        coinsHeld += coins;
        wallet.text = coinsHeld.ToString();
    }
    public void removeCoinsToWallet(int coins)
    {
        coinsHeld -= coins;
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

    public int returnCoin()
    {
        return coinsHeld;
    }
}
