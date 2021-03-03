using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreenManager : MonoBehaviour
{
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
            Debug.Log(vehicleToLoad);
        }
        SceneManager.LoadSceneAsync(2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
