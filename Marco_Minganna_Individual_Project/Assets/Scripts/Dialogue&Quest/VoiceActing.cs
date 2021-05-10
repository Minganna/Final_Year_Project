using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used to take track of the voice acting for the characters
/// </summary>
public class VoiceActing : MonoBehaviour
{
    [SerializeField]
    /// this variable keep track of the Audio source used to play the voice acting
    AudioSource VoiceLines;
    [SerializeField]
    /// string that is used to keep track of the path to the audio file
    string path;
    /// <summary>
    /// The script that keep track of the common static strings
    /// </summary>
    CommonVariables cv = new CommonVariables();
    /// <summary>
    /// the file that the audio source need to play
    /// </summary>
    AudioClip voice;
    // Start is called before the first frame update
    void Start()
    {
        VoiceLines = this.GetComponent<AudioSource>();

    }
    /// <summary>
    /// This function load the voice acting track from resources and play it 
    /// </summary>
    /// <param name="audio"></param>
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

    /// <summary>
    /// used to play the voice line when the button is pressed
    /// </summary>
    public void PlayVoiceLinebutton()
    {
        StopPreviousVoice();
        Debug.Log(voice);
        VoiceLines.PlayOneShot(voice);
    }

    /// <summary>
    /// prevent the character to play two audio files at the same time
    /// </summary>
    public void StopPreviousVoice()
    {
            VoiceLines.Stop();
       
    }


}
