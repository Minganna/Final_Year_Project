using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHasStillDialogue : MonoBehaviour
{
    [SerializeField]
    int remainingdialogues;
    [SerializeField]
    GameObject EsclamationMark;
    
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
