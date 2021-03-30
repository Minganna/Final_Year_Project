using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DecideGender : MonoBehaviour
{

    CommonVariables CV=new CommonVariables();
 public void SetGender(string gender)
    {
        CV.SetGender(gender);
    }    

    public void LoadNextScene()
    {
        SceneManager.LoadScene(2);
    }    
}
