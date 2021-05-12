using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Dialogue
{
    /// <summary>
    /// class that keep track of the NPC informations
    /// </summary>
    public class NPCDialogues : MonoBehaviour
    {

        Dialogue[] characterDialouges;
        /// <summary>
        /// the dialogues with and without subtitles in ita
        /// </summary>
        [SerializeField] Dialogue[] ItalianDialogue;
        /// <summary>
        /// the dialogues with and without subtitles in english
        /// </summary>
        [SerializeField] Dialogue[] EnglishDialogue;
        /// <summary>
        /// the name of the character
        /// </summary>
        [SerializeField] string converstantname;
        /// <summary>
        /// the avatar of the character
        /// </summary>
        [SerializeField] RenderTexture converstantAvatar;
        /// <summary>
        /// the voiceacting script 
        /// </summary>
        [SerializeField] VoiceActing voiceActing;
        /// <summary>
        /// The script that keep track of the common static strings
        /// </summary>
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
                //characterDialouges = EnglishDialogue;
                characterDialouges = ItalianDialogue;
            }
            

        }
        /// <summary>
        /// getter for the dialogues
        /// </summary>
        /// <returns></returns>
        public Dialogue[] GetDialogues()
        {
            return characterDialouges;
        }
        /// <summary>
        /// getter for the name variable
        /// </summary>
        /// <returns></returns>
        public string GetName()
        {
            return converstantname;
        }
        /// <summary>
        /// getter for the avatar texture
        /// </summary>
        /// <returns></returns>
        public RenderTexture GetAvatar()
        {
            return converstantAvatar;
        }
        /// <summary>
        /// getter for the voiceacting 
        /// </summary>
        /// <returns></returns>
        public VoiceActing GetVA()
        {
            return voiceActing;
        }
    }
}