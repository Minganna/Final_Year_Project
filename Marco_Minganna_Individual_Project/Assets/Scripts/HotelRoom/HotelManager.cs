using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HotelManager : MonoBehaviour
{
    [SerializeField]
    List<GameObject> Buttons;
    [SerializeField]
    List<GameObject> SnowGlobes;
    [SerializeField]
    List<GameObject> GlobesSuvenirs;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject suvenir in GlobesSuvenirs)
        {
            suvenir.transform.Rotate(0.0f, 0.7f, 0.0f);
        }
    }

    public void ShowGlobes()
    {
        foreach (GameObject snow in SnowGlobes)
        {
            snow.SetActive(true);
        }
        foreach (GameObject button in Buttons)
        {
            button.SetActive(false);
        }

    }
    public void ShowButtons()
    {
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
}
