using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// used to exit the game
/// </summary>
public class Exit : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
