using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    CommonVariables cv=new CommonVariables();

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

}
