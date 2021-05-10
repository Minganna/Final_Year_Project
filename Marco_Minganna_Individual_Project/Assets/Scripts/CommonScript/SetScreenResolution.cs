using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This class is used to set the resolution of the game as requested to ensure the UI works as expected
/// </summary>
public class SetScreenResolution : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ///set Iphone screen resoltion
        // Screen.SetResolution(496, 1080, true);
        Debug.Log(Screen.currentResolution.height);
        if(Screen.currentResolution.height==1080)
        {
            Screen.SetResolution(800, 1080, true);
        }
        else
        {
            Screen.SetResolution(488, 768, true);
        }
       
        
    }
}
