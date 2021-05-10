using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// class used to keep track all the static variables needed for the game
/// </summary>
public class CommonVariables
{
    /// <summary>
    /// the canvas used to display the dialogue
    /// </summary>
    public GameObject UICanvas;
    /// <summary>
    /// the canvas used to display the in games UI elements
    /// </summary>
    public GameObject GameCanvas;

    /// <summary>
    /// used to update the model in the loading screen
    /// </summary>
    static string transportation;
    /// <summary>
    /// keep track of the language the player is trying to learn
    /// </summary>
    static string languageToLearn;
    /// <summary>
    /// keep track of the language known by the player
    /// </summary>
    static string languageKnown;

    /// <summary>
    /// Used to determine if the player require translations
    /// </summary>
    static bool translations;
    /// <summary>
    /// used to keep track the scene that is needed to be load after the loading scene 
    /// </summary>
    static int sceneToLoad;
    /// <summary>
    /// Keep track of the player name
    /// </summary>
    static string Name;
    /// <summary>
    /// keep track of the player gender
    /// </summary>
    static string Gender;

    /// <summary>
    /// setter for the name variable
    /// </summary>
    /// <param name="name"></param>
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

    /// <summary>
    /// setter for the translation variable
    /// </summary>
    /// <param name="state"></param>
    public void SetTranslation(bool state)
    {
        translations = state;
    }
    /// <summary>
    /// setter for the sceneToLoad variable
    /// </summary>
    /// <param name="scene"></param>
    public void setSceneToLoad(int scene)
    {
        sceneToLoad = scene;
    }
    /// <summary>
    /// getter for sceneToLoad variable
    /// </summary>
    /// <returns></returns>
    public int getSceneToLoad()
    {
        return sceneToLoad;
    }
    /// <summary>
    /// setter for the languageToLearn variable
    /// </summary>
    /// <param name="ltl"></param>
    public void SetLearn(string ltl)
    {
        languageToLearn = ltl;
    }
    /// <summary>
    /// setter for the languageKnown variable
    /// </summary>
    /// <param name="lk"></param>
    public void SetKnown(string lk)
    {
        languageKnown = lk;

    }

    /// <summary>
    /// setter for the gender variable
    /// </summary>
    /// <param name="gender"></param>
    public void SetGender(string gender)
    {
        Gender = gender;
    }

    /// <summary>
    /// getter for the languageToLearn variable
    /// </summary>
    /// <returns></returns>
    public string GetLearn()
    {
        return languageToLearn;
    }

    /// <summary>
    /// getter for the languageKnown variable
    /// </summary>
    /// <returns></returns>
    public string GetKnown()
    {
        return languageKnown;
    }

    /// <summary>
    /// setter for the transportation variable
    /// </summary>
    /// <param name="vehicle"></param>
    public void setTransp(string vehicle)
    {
        transportation = vehicle;
    }

    /// <summary>
    /// getter for the transportation variable
    /// </summary>
    /// <returns></returns>
    public string getTransp()
    {
        return transportation;
    }

    /// <summary>
    /// getter for the gender variable
    /// </summary>
    /// <returns></returns>
    public string getGender()
    {
        return Gender;
    }

    /// <summary>
    /// getter for the translations variable
    /// </summary>
    /// <returns></returns>
    public bool getTranslation()
    {
        return translations;
    }


    /// <summary>
    /// getter for the player name, if the variable is empty assign it to Player
    /// </summary>
    /// <returns></returns>
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
