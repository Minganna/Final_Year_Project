using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Core;

/// <summary>
/// Class used to keep track of the player coins
/// </summary>
public class Wallet : MonoBehaviour,IpredicateEvaluator
{
    /// <summary>
    /// The variable that stores the Ui element for keeping track of the coins
    /// </summary>
    public TextMeshProUGUI wallet;
    static int coinsHeld=0;

    private void Start()
    {
        wallet.text = coinsHeld.ToString();
    }
    /// <summary>
    /// function used to add the reward coins when a quest is completed
    /// </summary>
    /// <param name="coins"></param>
    public void addCoinsToWallet(int coins)
    {
        coinsHeld += coins;
        wallet.text = coinsHeld.ToString();
    }
    /// <summary>
    /// function used to remove spent coins from the wallet
    /// </summary>
    /// <param name="coins"></param>
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
    /// <summary>
    /// getter of the coins
    /// </summary>
    /// <returns></returns>
    public int returnCoin()
    {
        return coinsHeld;
    }
}
