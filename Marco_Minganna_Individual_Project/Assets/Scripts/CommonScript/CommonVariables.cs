using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonVariables
{
    public GameObject UICanvas;
    public GameObject GameCanvas;

    static string transportation;

    static string languageToLearn;
    static string languageKnown;

    static bool translations;
    static int sceneToLoad;

    static string Name;
    static string Gender;


    public void SetName(string name)
    {
        Debug.Log(name);
        if(name=="")
        {
            Name = "Player";
        }
        else
        {
            Name = name;
        } 
    }

    public void SetTranslation(bool state)
    {
        translations = state;
    }

    public void setSceneToLoad(int scene)
    {
        sceneToLoad = scene;
    }

    public int getSceneToLoad()
    {
        return sceneToLoad;
    }

    public void SetLearn(string ltl)
    {
        languageToLearn = ltl;
    }
    public void SetKnown(string lk)
    {
        languageKnown = lk;

    }

    public void SetGender(string gender)
    {
        Gender = gender;
    }

    public string GetLearn()
    {
        return languageToLearn;
    }
    public string GetKnown()
    {
        return languageKnown;
    }

    public void setTransp(string vehicle)
    {
        transportation = vehicle;
    }
    public string getTransp()
    {
        return transportation;
    }

    public string getGender()
    {
        return Gender;
    }

    public bool getTranslation()
    {
        return translations;
    }

    public string getName()
    {
        if (Name != null)
        {
            return Name;
        }
        else
        {
            return "Player";
        }
    }

}
