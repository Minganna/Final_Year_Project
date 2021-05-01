using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Dialogue
{
    public class NPCDialogues : MonoBehaviour
    {
        Dialogue[] characterDialouges;
        [SerializeField] Dialogue[] ItalianDialogue;
        [SerializeField] Dialogue[] EnglishDialogue;
        [SerializeField] string converstantname;
        [SerializeField] RenderTexture converstantAvatar;
        [SerializeField] VoiceActing voiceActing;
        CommonVariables cv = new CommonVariables();


        private void Start()
        {
            if(cv.GetLearn()!=null)
            {
                if (cv.GetLearn().Trim().ToLower().Contains("english"))
                {
                    characterDialouges = ItalianDialogue;
                }
                if (cv.GetLearn().Trim().ToLower().Contains("italian"))
                {
                    characterDialouges = EnglishDialogue;
                }
            }
            else
            {
                characterDialouges = EnglishDialogue;
                //characterDialouges = ItalianDialogue;
            }
            

        }
        public Dialogue[] GetDialogues()
        {
            return characterDialouges;
        }

        public string GetName()
        {
            return converstantname;
        }

        public RenderTexture GetAvatar()
        {
            return converstantAvatar;
        }

        public VoiceActing GetVA()
        {
            return voiceActing;
        }
    }
}