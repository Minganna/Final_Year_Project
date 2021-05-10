using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using Core;

namespace Dialogue
{
    public class DialogueNode:ScriptableObject
    {
        [SerializeField]
        /// variable used to identify if the player is speaking
        bool PlayerSpeaking=false;
        [SerializeField]
        ///variable used to identify if the AI is asking a question
        bool Question = false;
        [SerializeField]
        ///The main variable of the node, it contains the dialogue text
        string Text;
        [SerializeField]
        /// Used to link the node to the correct voice track
        string voicetrack;
        [SerializeField]
        ///list of children for the node
        List<string> children=new List<string>();
        [SerializeField]
        ///used to identify the position of the node object in the editor
        Rect position=new Rect(0,0,250,130);
        [SerializeField]
        ///used to call an action when entering this node
        string OnEnterAction;
        [SerializeField]
        /// used to call an action when leaving this node
        string OnExitAction;
        [SerializeField]
        /// used to specify if this node needs a condition to be met to be entered (player has or has completed a quest)
        Condition condition;

        /// <summary>
        /// getter for the position of the node in the editor
        /// </summary>
        /// <returns></returns>
        public Rect GetRect()
        {
            return position;
        }

        /// <summary>
        /// getter for the dialogue phrase
        /// </summary>
        /// <returns></returns>
        public string GetText()
        {
            return Text;
        }

        /// <summary>
        /// getter for the voice track reference
        /// </summary>
        /// <returns></returns>
        public string GetVoiceTrack()
        {
            return voicetrack;
        }

        /// <summary>
        /// getter for the nodes linked to this node
        /// </summary>
        /// <returns></returns>
        public List<string> GetChildren()
        {
            return children;
        }

        /// <summary>
        /// getter for the variable that determine who is speaking
        /// </summary>
        /// <returns></returns>
        public bool isPlayerSpeaking()
        {
            return PlayerSpeaking;
        }

        /// <summary>
        /// getter for the variable that is determining if the node contain a question
        /// </summary>
        /// <returns></returns>
        public bool IsQuestion()
        {
            return Question;
        }

        /// <summary>
        /// Getter for the string that contain the required enter action nomenclature 
        /// </summary>
        /// <returns></returns>
        public string GetOnEnterAction()
        {
            return OnEnterAction;
        }
        /// <summary>
        /// Getter for the string that contain the required exit action nomenclature
        /// </summary>
        /// <returns></returns>
        public string GetOnExitAction()
        {
            return OnExitAction;
        }
        /// <summary>
        /// Function used to check the condition to enter the node
        /// </summary>
        /// <param name="evaluators"></param>
        /// <returns></returns>
        public bool checkCondition(IEnumerable<IpredicateEvaluator> evaluators)
        {
            return condition.check(evaluators);
        }


#if UNITY_EDITOR
        /// <summary>
        /// Setter for the node position
        /// </summary>
        /// <param name="newPosition"></param>
        public void SetPosition(Vector2 newPosition)
        {
            Undo.RecordObject(this, "Move dialogue objects");
            position.position = newPosition;
            EditorUtility.SetDirty(this);
        }
        /// <summary>
        /// Setter for the dialogue text
        /// </summary>
        /// <param name="newText"></param>
        public void SetText(string newText)
        {
            if(newText!=Text)
            {
                Undo.RecordObject(this, "Update Dialogue Text");
                Text = newText;
                EditorUtility.SetDirty(this);
            }
        }
        /// <summary>
        /// setter for the voice acting linkage
        /// </summary>
        /// <param name="newText"></param>
        public void SetVoiceActing(string newText)
        {
            if (newText != voicetrack)
            {
                Undo.RecordObject(this, "Update Dialogue Text");
                voicetrack = newText;
                EditorUtility.SetDirty(this);
            }
        }
        /// <summary>
        /// set to add and link a node to the node
        /// </summary>
        /// <param name="childId"></param>
        public void AddChild(string childId)
        {
            Undo.RecordObject(this, "Add Dialogue link");
            children.Add(childId);
            EditorUtility.SetDirty(this);
        }

        /// <summary>
        /// used to remove a linkage between two nodes
        /// </summary>
        /// <param name="childId"></param>
        public void RemoveChild(string childId)
        {
            Undo.RecordObject(this, "Remove Dialogue link");
            children.Remove(childId);
            EditorUtility.SetDirty(this);
        }

        /// <summary>
        /// use to change the speaker in the dialogue node between player and AI
        /// </summary>
        /// <param name="newIsPlayerSpeaking"></param>
        public void SetSpeaker(bool newIsPlayerSpeaking)
        {
            Undo.RecordObject(this, "Change Dialogue Speaker");
            PlayerSpeaking = newIsPlayerSpeaking;
            EditorUtility.SetDirty(this);
        }

        /// <summary>
        /// Used to set a node as a question
        /// </summary>
        /// <param name="status"></param>
        public void SetIsQuestion(bool status)
        {
            Question = status;
        }


#endif
    }
}
