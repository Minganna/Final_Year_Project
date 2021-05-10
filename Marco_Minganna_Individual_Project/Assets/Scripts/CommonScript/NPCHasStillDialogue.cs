using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this class is used to detect if the player has still something to say to the player
/// </summary>
public class NPCHasStillDialogue : MonoBehaviour
{
    [SerializeField]
    ///the number of possible dialogue the NPC still has
    int remainingdialogues;
    [SerializeField]
    ///the esclamation mark that mark that the player has still something to say
    GameObject EsclamationMark;
    
    /// <summary>
    /// Function used to remove the esclamation mark when all the possible dialogue with a character is read
    /// </summary>
    public void readDialogue()
    {
        if(remainingdialogues>0)
        {
            remainingdialogues -= 1;
        }
        if(remainingdialogues==0)
        {
            EsclamationMark.SetActive(false);
        }
    }

}
