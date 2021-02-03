using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace Dialogue
{
    public class PlayerConversant : MonoBehaviour
    {

        [SerializeField]string playerName;
        [SerializeField] RenderTexture playerAvatar;
        Dialogue currentDialogue;
        DialogueNode currentNode = null;
        NPCDialogues currentConverstant = null;
        bool isChoosing = false;

        public event Action onConversationUpdated;


        public void StartDialogue(NPCDialogues newConversant, Dialogue newDialogue)
        {
            currentConverstant = newConversant;
            currentDialogue = newDialogue;
            newDialogue.OnValidate();
            currentNode = currentDialogue.GetRootNode();
            TriggerEnterAction();
            onConversationUpdated();
        }

        public string GetCurrentConversantName()
        {
            if(isChoosing)
            {
                return playerName;
            }
            else
            {
                return currentConverstant.GetName();
            }
        }


        public RenderTexture GetCurrentAvatar()
        {
            if (isChoosing)
            {
                return playerAvatar;
            }
            else
            {
                return currentConverstant.GetAvatar();
            }
        }

        public void Quit()
        {
            TriggerQuitExitAction();
            currentConverstant = null;
            currentDialogue = null;
            currentNode = null;
            isChoosing = false;
            onConversationUpdated();
        }

        public bool isActive()
        {
            return currentDialogue != null;
        }

        public bool IsChoosing()
        {
            return isChoosing;
        }

        public string GetText()
        {
            if(currentNode==null)
            {
                return "";
            }
            else
            {
               return currentNode.GetText();
            }    
        }

        public bool GetIsQuestion()
        {
            if (currentNode == null)
            {
                return false;
            }
            else
            {
                return currentNode.IsQuestion();
            }
        }

        public void SelectChoice(DialogueNode chosenNode)
        {
            currentNode = chosenNode;
            TriggerEnterAction();
            isChoosing = false;
        }



        public void Next()
        {

            int numPlayerResponses = currentDialogue.GetPlayerChildren(currentNode).Count();
            if (numPlayerResponses > 0)
            {
                isChoosing = true;
                TriggerExitAction();
                onConversationUpdated();
                return;
            }
        
            DialogueNode[] children= currentDialogue.GetAllChildren(currentNode).ToArray();
            int RandomIndex = UnityEngine.Random.Range(0, children.Count());
            TriggerExitAction();
            currentNode= children[RandomIndex];
            TriggerEnterAction();
            onConversationUpdated();
        }

        public IEnumerable<DialogueNode> GetChoices()
        {
            return currentDialogue.GetPlayerChildren(currentNode);
        }


        public bool hasNext()
        {
            return currentDialogue.GetAllChildren(currentNode).Count()>0;
        }

        private void TriggerEnterAction()
        {
            if(currentNode!= null)
            {
                TriggerAction(currentNode.GetOnEnterAction());
            }
        }
        private void TriggerExitAction()
        {
            if (currentNode != null)
            {

                    TriggerAction(currentNode.GetOnExitAction());

              
            }
        }

        private void TriggerQuitExitAction()
        {
            if (currentNode != null)
            {
                if (currentNode.GetOnExitAction() == "")
                {
                    TriggerAction("show");
                }
                else
                {
                    TriggerAction(currentNode.GetOnExitAction());
                }

            }
        }

        private void TriggerAction(string action)
        {
            if (action == "") return;

            foreach(DialogueTrigger trigger in currentConverstant.GetComponents<DialogueTrigger>())
            {
                trigger.Trigger(action);
            }
        }
    }
}
