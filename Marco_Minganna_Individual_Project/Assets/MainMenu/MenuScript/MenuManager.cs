using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;
public class MenuManager : MonoBehaviour
{
    private List<GameObject> PlaySettings=new List<GameObject>();
    public List<GameObject> SelectLanguage = new List<GameObject>();
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
                string[] name = lui.name.Split('(');
                datafromdropdown.Add(name[0]+": "+menuOptions[index].text);


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
        Debug.Log(SelectedValues[0]);
        Debug.Log(SelectedValues[1]);


    }
}
