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
    AudioClip voice;
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
                string currentpath = "VoiceActing/" + path + "/" + cv.GetLearn().Trim().ToLower() + "/" + audio;
                voice = Resources.Load(currentpath, typeof(AudioClip)) as AudioClip;
                VoiceLines.PlayOneShot(voice);
            }
            else
            {
                 voice = Resources.Load("VoiceActing/" + path + "/" + "english" + "/" + audio, typeof(AudioClip)) as AudioClip;
                //AudioClip voice = Resources.Load("VoiceActing/" + path + "/" + "italian" + "/" + audio, typeof(AudioClip)) as AudioClip;
                VoiceLines.PlayOneShot(voice);
            }
           
        }
      
    }

    public void PlayVoiceLinebutton()
    {
        StopPreviousVoice();
        Debug.Log(voice);
        VoiceLines.PlayOneShot(voice);
    }


    public void StopPreviousVoice()
    {
            VoiceLines.Stop();
       
    }


}
