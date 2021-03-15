using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    CommonVariables cv=new CommonVariables();
    [SerializeField] TextMesh Departures;
    [SerializeField] bool isArrival;


    public void SetTranslation(bool state)
    {
        cv.SetTranslation(state);
    }

    public void NextScene()
    {
        cv.setTransp("plane");
        cv.setSceneToLoad(3);
        SceneManager.LoadScene(1);
    }

    private void Start()
    {
        if (cv.GetLearn() != null)
        {
            if (cv.GetLearn().Trim().ToLower().Contains("english"))
            {
                if(isArrival)
                {
                    Departures.text = "Arrivi";
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
                    Departures.text = "Arrival";
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
                Departures.text = "Arrival";
            }
            else
            {

                Departures.text = "Departures";
            }
        }
    }

}
