using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceActing : MonoBehaviour
{
    [SerializeField]
    AudioSource VoiceLines;
    [SerializeField]
    string path;
    CommonVariables cv = new CommonVariables();
    // Start is called before the first frame update
    void Start()
    {
        VoiceLines = this.GetComponent<AudioSource>();

    }

    public void PlayVoiceLine(string audio)
    {
        if(audio!="")
        {
            if(cv.GetLearn()!=null)
            {
                Debug.Log(cv.GetLearn().Trim().ToLower());
                AudioClip voice = Resources.Load("VoiceActing/" + path + "/" + cv.GetLearn().Trim().ToLower() + "/" + audio, typeof(AudioClip)) as AudioClip;
                VoiceLines.PlayOneShot(voice);
            }
            else
            {
                AudioClip voice = Resources.Load("VoiceActing/" + path + "/" + "english" + "/" + audio, typeof(AudioClip)) as AudioClip;
                //AudioClip voice = Resources.Load("VoiceActing/" + path + "/" + "italian" + "/" + audio, typeof(AudioClip)) as AudioClip;
                VoiceLines.PlayOneShot(voice);
            }
           
        }
      
    }

    public void StopPreviousVoice()
    {
            VoiceLines.Stop();
       
    }


}
