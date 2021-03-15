using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreenManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] vehicles;
    CommonVariables cv=new CommonVariables();
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

    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadSceneAsync(cv.getSceneToLoad());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
