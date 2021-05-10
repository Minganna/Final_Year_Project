using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// class used to define the player gender 
/// </summary>
public class DecideGender : MonoBehaviour
{
    /// <summary>
    /// The script that keep track of the common static strings
    /// </summary>
    CommonVariables CV =new CommonVariables();
    /// <summary>
    /// setter for the gender variable
    /// </summary>
    /// <param name="gender"></param>
 public void SetGender(string gender)
    {
        CV.SetGender(gender);
    }    
    /// <summary>
    /// used to load the next scene
    /// </summary>
    public void LoadNextScene()
    {
        SceneManager.LoadScene(2);
    }    
}
