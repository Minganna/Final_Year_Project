using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
/// <summary>
/// the main menu manager class
/// </summary>
public class MenuManager : MonoBehaviour
{
    /// <summary>
    /// list of the main items showed in the menu-play and settings
    /// </summary>
    private List<GameObject> PlaySettings=new List<GameObject>();
    /// <summary>
    /// list of the menu items related to the language they know and looking to learn
    /// </summary>
    public List<GameObject> SelectLanguage = new List<GameObject>();
    /// <summary>
    ///  list of confirmation buttons
    /// </summary>
    public List<GameObject> Confirm = new List<GameObject>();
    /// <summary>
    /// variable that stores the name of the player
    /// </summary>
    public TMP_InputField Name;
    /// <summary>
    /// The script that keep track of the common static strings
    /// </summary>
    CommonVariables cv = new CommonVariables();
    /// <summary>
    /// function used to determine the action to take when the button is pressed
    /// </summary>
    /// <param name="tag"></param>
    public void MainButtons(GameObject tag)
    {
        GameObject maincanvas = GameObject.FindGameObjectWithTag("MasterMenu");
        switch(tag.name.ToString())
        {
            case "Play":
                PlayPressed(maincanvas,tag);
                break;
            case "Back(Clone)":
                BackToMainMenu(maincanvas,tag);
                break;
            case "Next(Clone)":
                NextPressed(maincanvas);
                break;
            case "Confirm(Clone)":
                cv.SetName(Name.text);
                Debug.Log(cv.GetLearn());
                if(cv.GetLearn()=="English"|| cv.GetLearn() == "Italian")
                {
                    cv.setSceneToLoad(3);
                    SceneManager.LoadScene(1);
                }   
                else
                {
                    BackToMainMenu(maincanvas, tag);
                }
                break;
            default:
                break;

        }

       
    }
    /// <summary>
    /// function used to deactivate the menu part needed
    /// </summary>
    /// <param name="tag"></param>
    void DeactivateStartandSettings(GameObject tag)
    {
        GameObject[] menuitems = GameObject.FindGameObjectsWithTag(tag.tag);
        foreach (GameObject menu in menuitems)
        {
            menu.SetActive(false);
        }
        if (PlaySettings.Count == 0)
        {
            foreach (GameObject menu in menuitems)
            {
                PlaySettings.Add(menu);
            }
        }
    }
    /// <summary>
    /// used to destroy the menu items needed
    /// </summary>
    /// <returns></returns>
    List<string> DestroymenuItems()
    {
        GameObject[] LanguageUI = GameObject.FindGameObjectsWithTag("LanguageMenu");
        List<string> datafromdropdown = new List<string>();
        foreach (GameObject lui in LanguageUI)
        {
            if (lui.GetComponent<TMP_Dropdown>() != null)
            {
                TMP_Dropdown dpd = lui.GetComponent<TMP_Dropdown>();
                int index = dpd.value;
                List<TMP_Dropdown.OptionData> menuOptions = dpd.options;
                datafromdropdown.Add(menuOptions[index].text);


            }
                Destroy(lui);
        }
        GameObject[] backandNext = GameObject.FindGameObjectsWithTag("MainMenuMainSettings");
        foreach (GameObject lui in backandNext)
        {
            Destroy(lui);
        }
        return datafromdropdown;
    }
    /// <summary>
    /// used to go back to the main menu
    /// </summary>
    /// <param name="maincanvas"></param>
    /// <param name="pressed"></param>
    void BackToMainMenu(GameObject maincanvas,GameObject pressed)
    {
        DestroymenuItems();
        foreach (GameObject mainmenu in PlaySettings)
        {
            mainmenu.SetActive(true);
        }
    }

    /// <summary>
    /// function called when the play button is pressed
    /// </summary>
    /// <param name="maincanvas"></param>
    /// <param name="tag"></param>
    void PlayPressed(GameObject maincanvas,GameObject tag)
    {
        DeactivateStartandSettings(tag);
        if (SelectLanguage.Count != 0)
        {
            for (int i = 0; i < SelectLanguage.Count; i++)
            {
                GameObject newButton = Instantiate(SelectLanguage[i]) as GameObject;

                newButton.transform.SetParent(maincanvas.transform, false);
                if (SelectLanguage[i].GetComponent<Button>()!=null)
                {
                    Button Back = newButton.GetComponent<Button>();
                    Back.onClick.AddListener(delegate { MainButtons(newButton); });
                }
                else
                {
                    newButton.tag = "LanguageMenu";
                }
                newButton.SetActive(true);
            }

        }
    }
    /// <summary>
    /// function called when the next button is pressed
    /// </summary>
    /// <param name="maincanvas"></param>
    void NextPressed(GameObject maincanvas)
    {
        List<string> SelectedValues = new List<string>();
        SelectedValues= DestroymenuItems();
        cv.SetLearn(SelectedValues[0]);
        cv.SetKnown(SelectedValues[1]);
        cv.setTransp("car");
        ConfirmationPart();
    }
    /// <summary>
    /// called to show the last part of the menu, where the player need to confirm that the info are correct
    /// </summary>
    private void ConfirmationPart()
    {
        GameObject maincanvas = GameObject.FindGameObjectWithTag("MasterMenu");
        for (int i = 0; i < SelectLanguage.Count; i++)
        {
            GameObject newButton = Instantiate(Confirm[i]) as GameObject;
            newButton.transform.SetParent(maincanvas.transform, false);
            if (Confirm[i].GetComponent<Button>() != null)
            {
                Button Back = newButton.GetComponent<Button>();
                Back.onClick.AddListener(delegate { MainButtons(newButton); });
            }
            else
            {
                newButton.tag = "LanguageMenu";
                if(newButton.name== "template1(Clone)")
                {
                    newButton.GetComponent<TextMeshProUGUI>().text = cv.GetKnown();
                }
                if (newButton.name == "template2(Clone)")
                {
                    newButton.GetComponent<TextMeshProUGUI>().text = cv.GetLearn();
                }
            }
            newButton.SetActive(true);

        }
    }
}
