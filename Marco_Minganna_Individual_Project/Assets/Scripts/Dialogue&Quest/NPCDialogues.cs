using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Dialogue
{
    public class NPCDialogues : MonoBehaviour
    {
        [SerializeField] Dialogue[] characterDialouges;
        [SerializeField] string converstantname;
        [SerializeField] RenderTexture converstantAvatar;


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
    }
}