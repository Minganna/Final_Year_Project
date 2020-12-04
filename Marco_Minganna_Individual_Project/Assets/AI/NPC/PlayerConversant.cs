using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue
{
    public class PlayerConversant : MonoBehaviour
    {
        [SerializeField] Dialogue currentDialogue;

        public string GetText()
        {
            if(currentDialogue==null)
            {
                return "";
            }
            else
            {
               return currentDialogue.GetRootNode().GetText();
            }    
        }

        public bool GetIsQuestion()
        {
            if (currentDialogue == null)
            {
                return false;
            }
            else
            {
                return currentDialogue.GetRootNode().IsQuestion();
            }
        }

    }
}
