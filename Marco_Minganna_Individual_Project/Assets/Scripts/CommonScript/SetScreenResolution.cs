using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetScreenResolution : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ///set Iphone screen resoltion
       // Screen.SetResolution(496, 1080, true);
        Screen.SetResolution(800, 1080, true);
    }
}
