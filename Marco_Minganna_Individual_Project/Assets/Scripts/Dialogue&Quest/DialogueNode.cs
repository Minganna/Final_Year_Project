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
        bool PlayerSpeaking=false;
        [SerializeField]
        bool Question = false;
        [SerializeField]
        string Text;
        [SerializeField]
        string voicetrack;
        [SerializeField]
        List<string> children=new List<string>();
        [SerializeField]
        Rect position=new Rect(0,0,250,130);
        [SerializeField]
        string OnEnterAction;
        [SerializeField]
        string OnExitAction;
        [SerializeField]
        Condition condition;

        public Rect GetRect()
        {
            return position;
        }

        public string GetText()
        {
            return Text;
        }

        public string GetVoiceTrack()
        {
            return voicetrack;
        }

        public int GetVoiceTrackint()
        {
            return int.Parse(voicetrack);
        }

        public List<string> GetChildren()
        {
            return children;
        }

        public bool isPlayerSpeaking()
        {
            return PlayerSpeaking;
        }

        public bool IsQuestion()
        {
            return Question;
        }

        public string GetOnEnterAction()
        {
            return OnEnterAction;
        }
        public string GetOnExitAction()
        {
            return OnExitAction;
        }

        public bool checkCondition(IEnumerable<IpredicateEvaluator> evaluators)
        {
            return condition.check(evaluators);
        }


#if UNITY_EDITOR
        public void SetPosition(Vector2 newPosition)
        {
            Undo.RecordObject(this, "Move dialogue objects");
            position.position = newPosition;
            EditorUtility.SetDirty(this);
        }

        public void SetText(string newText)
        {
            if(newText!=Text)
            {
                Undo.RecordObject(this, "Update Dialogue Text");
                Text = newText;
                EditorUtility.SetDirty(this);
            }
        }

        public void SetVoiceActing(string newText)
        {
            if (newText != voicetrack)
            {
                Undo.RecordObject(this, "Update Dialogue Text");
                voicetrack = newText;
                EditorUtility.SetDirty(this);
            }
        }
        public void AddChild(string childId)
        {
            Undo.RecordObject(this, "Add Dialogue link");
            children.Add(childId);
            EditorUtility.SetDirty(this);
        }

        public void RemoveChild(string childId)
        {
            Undo.RecordObject(this, "Remove Dialogue link");
            children.Remove(childId);
            EditorUtility.SetDirty(this);
        }

        public void SetSpeaker(bool newIsPlayerSpeaking)
        {
            Undo.RecordObject(this, "Change Dialogue Speaker");
            PlayerSpeaking = newIsPlayerSpeaking;
            EditorUtility.SetDirty(this);
        }

        public void SetIsQuestion(bool status)
        {
            Question = status;
        }


#endif
    }
}
