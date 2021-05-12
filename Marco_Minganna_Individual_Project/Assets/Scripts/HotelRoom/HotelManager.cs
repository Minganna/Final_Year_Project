using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// this script is used as a manager for the Hotel scene
/// </summary>
public class HotelManager : MonoBehaviour
{
    [SerializeField]
    ///the list of buttons of the scene to visit
    List<GameObject> Buttons;
    [SerializeField]
    ///list of snowglobes in the snowglobe section
    List<GameObject> SnowGlobes;
    [SerializeField]
    ///the actual game souvenirs
    List<GameObject> GlobesSuvenirs;
    [SerializeField]
    ///the render teture linked to the globes
    List<Texture> globeTex;
    [SerializeField]
    /// the panels that contain the data (buttons, souvenirs and translations)
    List<GameObject> Panels;
    /// <summary>
    /// The script that keep track of the common static strings
    /// </summary>
    CommonVariables cv = new CommonVariables();

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject suvenir in GlobesSuvenirs)
        {
            suvenir.transform.Rotate(0.0f, 0.7f, 0.0f);
        }
    }
    /// <summary>
    /// Used to load the turisting spot, colosseum in Italy and Stonehenge in UK
    /// </summary>
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
    /// <summary>
    /// used to load the snowglobes
    /// </summary>
    public void ShowGlobes()
    {
        ShowPanels(0);
        ShowPanels(2);
        foreach (GameObject snow in SnowGlobes)
        {
            snow.SetActive(true);
        }
        foreach (GameObject button in Buttons)
        {
            button.SetActive(false);
        }

    }
    /// <summary>
    /// use to load the correct panel to display the right informations
    /// </summary>
    /// <param name="panel"></param>
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
        if (panel == 3)
        {
            Panels[panel].SetActive(true);
            Panels[panel - 1].SetActive(false);
        }
        if (panel == 2)
        {
            Panels[panel].SetActive(true);
            Panels[panel + 1].SetActive(false);
        }

    }
    /// <summary>
    /// used to load the buttons with the location the player can visit
    /// </summary>
    public void ShowButtons()
    {
        ShowPanels(0);
        ShowPanels(2);
        foreach (GameObject snow in SnowGlobes)
        {
            snow.SetActive(false);
        }
        foreach(GameObject button in Buttons)
        {
            button.SetActive(true);
        }

    }
    /// <summary>
    /// used to travel to a specific scene
    /// </summary>
    /// <param name="areaNumber"></param>
    public void GoToArea(int areaNumber)
    {
        SceneManager.LoadScene(areaNumber);

    }
    /// <summary>
    /// used to show the render texture of the snowglobe linked to the image
    /// </summary>
    /// <param name="ButtonPressed"></param>
    public void showSnowGlobe(GameObject ButtonPressed)
    {
        Wallet wallet= FindObjectOfType<Wallet>();
        int coinsHeld = wallet.returnCoin();
        if(coinsHeld>=int.Parse(ButtonPressed.GetComponentInChildren<TextMeshProUGUI>().text))
        {
            wallet.removeCoinsToWallet(int.Parse(ButtonPressed.GetComponentInChildren<TextMeshProUGUI>().text));
            ButtonPressed.SetActive(false);
            RawImage globeUnlocked = SnowGlobes[int.Parse(ButtonPressed.name)].GetComponent<RawImage>();
            if(int.Parse(ButtonPressed.name)<4)
            {
                globeUnlocked.texture = globeTex[int.Parse(ButtonPressed.name)];
            }
            else
            {
                globeUnlocked.texture = globeTex[int.Parse(ButtonPressed.name)-4];
            }
           
        }
    }
}
