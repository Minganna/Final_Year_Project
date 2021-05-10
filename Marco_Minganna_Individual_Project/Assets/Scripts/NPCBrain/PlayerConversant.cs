using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Core;

namespace Dialogue
{
    /// <summary>
    /// This class is used to keep track of the status of the current dialogue
    /// </summary>
    public class PlayerConversant : MonoBehaviour
    {
        /// <summary>
        /// Variable that keep track of the player name
        /// </summary>
        [SerializeField]string playerName;
        /// <summary>
        /// Variable that keep track of the player avatar render texture
        /// </summary>
        [SerializeField] RenderTexture playerAvatar;
        /// <summary>
        /// The current dialogue 
        /// </summary>
        Dialogue currentDialogue;
        /// <summary>
        /// the current dialogue node 
        /// </summary>
        DialogueNode currentNode = null;
        /// <summary>
        /// Variable that keep track of the AI conversant 
        /// </summary>
        NPCDialogues currentConverstant = null;
        /// <summary>
        /// Variable that is keeping track if the player is choosing an answer for a question
        /// </summary>
        bool isChoosing = false;
        /// <summary>
        /// Class that keep track of the common static variables
        /// </summary>
        CommonVariables cv = new CommonVariables();
        /// <summary>
        /// Used to call a DialogueUI function
        /// </summary>
        public event Action onConversationUpdated;
        /// <summary>
        /// A string that keeps track of the current voiceActing line
        /// </summary>
        string VoiceActingline;
        /// <summary>
        /// the Voiceacting manager script
        /// </summary>
        VoiceActing va;


        /// <summary>
        /// This function is used to start the dialogue
        /// </summary>
        /// <param name="newConversant"></param>
        /// <param name="newDialogue"></param>
        public void StartDialogue(NPCDialogues newConversant, Dialogue newDialogue)
        {
            playerName = cv.getName();
            currentConverstant = newConversant;
            currentDialogue = newDialogue;
            if(newConversant.GetVA()!=null)
            {
                va = newConversant.GetVA();
            }    
            newDialogue.OnValidate();
            currentNode = currentDialogue.GetRootNode();
            TriggerEnterAction();
            onConversationUpdated();
        }

        /// <summary>
        /// Function used to play the voice line linked to the current text
        /// </summary>
        public void PlayVoice()
        {
            if(va!=null)
            {
                va.StopPreviousVoice();
                VoiceActingline = currentNode.GetVoiceTrack();
                va.PlayVoiceLine(VoiceActingline);
            }    
        }
        /// <summary>
        /// Function used to update the name displayed based on who is talking
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Function used to update the avatar based on who is talking
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Function used to exit the dialogue and close the UI
        /// </summary>
        public void Quit()
        {
            TriggerQuitExitAction();
            currentConverstant = null;
            currentDialogue = null;
            currentNode = null;
            isChoosing = false;
            onConversationUpdated();
        }
        /// <summary>
        /// Used to make sure that a dialogue is stored in the dialogue variable
        /// </summary>
        /// <returns></returns>
        public bool isActive()
        {
            return currentDialogue != null;
        }
        /// <summary>
        /// getter for the isChoosing variable
        /// </summary>
        /// <returns></returns>
        public bool IsChoosing()
        {
            return isChoosing;
        }

        /// <summary>
        /// used to get the dialogue text
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// getter for the isQuestion variable
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Getter for the variable that store the name of the voiceline track
        /// </summary>
        /// <returns></returns>
        public string GetVoiceActingline()
        {

            if (currentNode == null)
            {
                return "";
            }
            else
            {
                return currentNode.GetVoiceTrack();
            }
        }
        /// <summary>
        /// used to select the node that the player choose during a question
        /// </summary>
        /// <param name="chosenNode"></param>
        public void SelectChoice(DialogueNode chosenNode)
        {
            currentNode = chosenNode;
            TriggerEnterAction();
            isChoosing = false;
        }



        /// <summary>
        /// used to move to the next dialogue node
        /// </summary>
        public void Next()
        {

            int numPlayerResponses = filterOnCondition(currentDialogue.GetPlayerChildren(currentNode)).Count();
            if (numPlayerResponses > 0)
            {
                isChoosing = true;
                TriggerExitAction();
                onConversationUpdated();
                return;
            }
        
            DialogueNode[] children= filterOnCondition(currentDialogue.GetAIChildren(currentNode)).ToArray();
            int RandomIndex = UnityEngine.Random.Range(0, children.Count());
            TriggerExitAction();
            currentNode= children[RandomIndex];
            TriggerEnterAction();
            onConversationUpdated();
        }

        /// <summary>
        /// used to get all the possible choice when there is a question
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DialogueNode> GetChoices()
        {
            return filterOnCondition(currentDialogue.GetPlayerChildren(currentNode));
        }

        /// <summary>
        /// check if there is a child node connected to the current node
        /// </summary>
        /// <returns></returns>
        public bool hasNext()
        {
            return filterOnCondition(currentDialogue.GetAllChildren(currentNode)).Count()>0;
        }

        /// <summary>
        /// check if the conditions are meet
        /// </summary>
        /// <param name="inputNode"></param>
        /// <returns></returns>
        private IEnumerable<DialogueNode> filterOnCondition(IEnumerable<DialogueNode> inputNode)
        {
            foreach(var node in inputNode)
            {
                if(node.checkCondition(getEvaluators()))
                {
                    yield return node;
                }
            }    

        }

        private IEnumerable<IpredicateEvaluator> getEvaluators()
        {
            return GetComponents<IpredicateEvaluator>();
        }

        /// <summary>
        /// called when entering a node
        /// </summary>
        private void TriggerEnterAction()
        {
            if(currentNode!= null)
            {
                TriggerAction(currentNode.GetOnEnterAction());
            }
        }
        /// <summary>
        /// called when exiting a node
        /// </summary>
        private void TriggerExitAction()
        {
            if (currentNode != null)
            {

                    TriggerAction(currentNode.GetOnExitAction());

              
            }
        }
        /// <summary>
        /// called when closing a dialogue
        /// </summary>
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
        /// <summary>
        /// used to call a specific behaviour when needed
        /// </summary>
        /// <param name="action"></param>
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
