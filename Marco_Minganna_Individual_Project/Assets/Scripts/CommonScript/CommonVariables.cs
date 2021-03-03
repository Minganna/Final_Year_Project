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

    public void SetLearn(string ltl)
    {
        languageToLearn = ltl;
        Debug.Log("language to learn: " + languageToLearn);
    }
    public void SetKnown(string lk)
    {
        languageKnown = lk;
        Debug.Log("language known: " + languageKnown);
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


}
