using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreenManager : MonoBehaviour
{
    [SerializeField]
    ///list of all the possible vehicles used to travel in the next scene
    GameObject[] vehicles;
    /// <summary>
    /// The script that keep track of the common static strings
    /// </summary>
    CommonVariables cv =new CommonVariables();
    // Start is called before the first frame update
    void Start()
    {
        string vehicleToLoad = cv.getTransp();
        if(vehicleToLoad=="")
        {
            Debug.Log("Ops");
        }
        else
        {
            if(vehicleToLoad=="car")
            {
                vehicles[0].SetActive(true);
            }
            if (vehicleToLoad == "plane")
            {
                vehicles[1].SetActive(true);
            }
            if (vehicleToLoad == "train")
            {
                vehicles[2].SetActive(true);
            }

        }

        StartCoroutine(LoadNextScene());
        
    }

    /// <summary>
    /// Function that load the next scene, it is a courutine as the loading was too quick to show the animation of the vehicle
    /// </summary>
    /// <returns></returns>
    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadSceneAsync(cv.getSceneToLoad());
    }
}
