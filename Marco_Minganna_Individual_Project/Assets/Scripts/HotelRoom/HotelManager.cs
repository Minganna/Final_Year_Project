using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class HotelManager : MonoBehaviour
{
    [SerializeField]
    List<GameObject> Buttons;
    [SerializeField]
    List<GameObject> SnowGlobes;
    [SerializeField]
    List<GameObject> GlobesSuvenirs;
    [SerializeField]
    List<Texture> globeTex;
    [SerializeField]
    List<GameObject> Panels;

    CommonVariables cv = new CommonVariables();

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject suvenir in GlobesSuvenirs)
        {
            suvenir.transform.Rotate(0.0f, 0.7f, 0.0f);
        }
    }

    public void GotoTuristicScene()
    {
        if (cv.GetLearn() != null)
        {
            if (cv.GetLearn().Trim().ToLower().Contains("english"))
            {
                SceneManager.LoadScene(10);
            }
            if (cv.GetLearn().Trim().ToLower().Contains("italian"))
            {
                SceneManager.LoadScene(9);
            }
        }
        else
        {
            SceneManager.LoadScene(9);
        }
    }

    public void ShowGlobes()
    {
        ShowPanels(0);
        foreach (GameObject snow in SnowGlobes)
        {
            snow.SetActive(true);
        }
        foreach (GameObject button in Buttons)
        {
            button.SetActive(false);
        }

    }
    public void ShowPanels(int panel)
    {
       if(panel==1)
        {
            Panels[panel].SetActive(true);
            Panels[panel-1].SetActive(false);
        }
        if (panel == 0)
        {
            Panels[panel].SetActive(true);
            Panels[panel + 1].SetActive(false);
        }

    }
    public void ShowButtons()
    {
        ShowPanels(0);
        foreach(GameObject snow in SnowGlobes)
        {
            snow.SetActive(false);
        }
        foreach(GameObject button in Buttons)
        {
            button.SetActive(true);
        }

    }

    public void GoToArea(int areaNumber)
    {
        SceneManager.LoadScene(areaNumber);

    }

    public void showSnowGlobe(GameObject ButtonPressed)
    {
        Wallet wallet= FindObjectOfType<Wallet>();
        int coinsHeld = wallet.returnCoin();
        if(coinsHeld>=int.Parse(ButtonPressed.GetComponentInChildren<TextMeshProUGUI>().text))
        {
            wallet.removeCoinsToWallet(int.Parse(ButtonPressed.GetComponentInChildren<TextMeshProUGUI>().text));
            ButtonPressed.SetActive(false);
            RawImage globeUnlocked = SnowGlobes[int.Parse(ButtonPressed.name)].GetComponent<RawImage>();
            globeUnlocked.texture = globeTex[int.Parse(ButtonPressed.name)];
        }
    }
}
