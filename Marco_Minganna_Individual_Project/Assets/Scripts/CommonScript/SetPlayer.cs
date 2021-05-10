using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used to display the player avatar model
/// </summary>
public class SetPlayer : MonoBehaviour
{
    /// <summary>
    /// The script that keep track of the common static strings
    /// </summary>
    CommonVariables cv = new CommonVariables();

    /// <summary>
    /// the boy version of the player
    /// </summary>
    [SerializeField] GameObject Boy;
    /// <summary>
    /// the female version of the player
    /// </summary>
    [SerializeField] GameObject Girl;

    // Start is called before the first frame update
    void Start()
    {
        if(cv.getGender()=="Male")
        {
            Boy.SetActive(true);
        }
        else
        {
            Girl.SetActive(true);
        }
    }

}
