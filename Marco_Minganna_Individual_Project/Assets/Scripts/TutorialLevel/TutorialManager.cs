using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Manager script for the tutorial level
/// </summary>
public class TutorialManager : MonoBehaviour
{
    CommonVariables cv=new CommonVariables();
    [SerializeField] TextMesh Departures;
    /// <summary>
    /// change the text in the airport depending if is the arrival or departing airport
    /// </summary>
    [SerializeField] bool isArrival;


    public void SetTranslation(bool state)
    {
        cv.SetTranslation(state);
    }

    /// <summary>
    /// used to move to the next scene
    /// </summary>
    public void NextScene()
    {
        cv.setTransp("plane");
        cv.setSceneToLoad(4);
        SceneManager.LoadScene(2);
    }

    private void Start()
    {
        if (cv.GetLearn() != null)
        {
            if (cv.GetLearn().Trim().ToLower().Contains("english"))
            {
                if(isArrival)
                {
                    Departures.text = "Arrival";
                }
                else
                {
                    Departures.text = "Partenze";
                }
              
            }
            if (cv.GetLearn().Trim().ToLower().Contains("italian"))
            {
                if(isArrival)
                {
                    Departures.text = "Arrivi";
                }
                else
                {

                    Departures.text = "Departures";
                }    
               
            }
        }
        else
        {
            if (isArrival)
            {
                Departures.text = "Arrivi";
            }
            else
            {

                Departures.text = "Departures";
            }
        }
    }

}
