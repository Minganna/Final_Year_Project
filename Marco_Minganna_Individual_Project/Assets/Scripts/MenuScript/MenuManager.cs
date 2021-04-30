using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using TMPro;
public class MenuManager : MonoBehaviour
{
    private List<GameObject> PlaySettings=new List<GameObject>();
    public List<GameObject> SelectLanguage = new List<GameObject>();
    public List<GameObject> Confirm = new List<GameObject>();
    public TMP_InputField Name;
    CommonVariables cv = new CommonVariables();
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
                cv.setSceneToLoad(3);
                SceneManager.LoadScene(1);
                break;
            default:
                break;

        }

       
    }

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

    void BackToMainMenu(GameObject maincanvas,GameObject pressed)
    {
        DestroymenuItems();
        foreach (GameObject mainmenu in PlaySettings)
        {
            mainmenu.SetActive(true);
        }
    }


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

    void NextPressed(GameObject maincanvas)
    {
        List<string> SelectedValues = new List<string>();
        SelectedValues= DestroymenuItems();
        cv.SetLearn(SelectedValues[0]);
        cv.SetKnown(SelectedValues[1]);
        cv.setTransp("car");
        ConfirmationPart();
    }

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
