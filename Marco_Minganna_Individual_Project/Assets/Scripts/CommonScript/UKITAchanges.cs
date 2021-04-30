using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UKITAchanges : MonoBehaviour
{
    CommonVariables cv = new CommonVariables();
    [SerializeField]
    List<GameObject> UKObjects = new List<GameObject>();
    [SerializeField]
    List<GameObject> ITAObjects = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        if (cv.GetLearn() != null)
        {
            if (cv.GetLearn().Trim().ToLower().Contains("italian"))
            {
                foreach (GameObject ita in ITAObjects)
                {
                    ita.SetActive(true);
                }
                foreach (GameObject uk in UKObjects)
                {
                    uk.SetActive(false);
                }
            }
            if (cv.GetLearn().Trim().ToLower().Contains("english"))
            {
                foreach (GameObject ita in ITAObjects)
                {
                    ita.SetActive(false);
                }
                foreach (GameObject uk in UKObjects)
                {
                    uk.SetActive(true);
                }
            }
        }
        else
        {
          /*  foreach (GameObject ita in ITAObjects)
            {
                ita.SetActive(true);
            }
            foreach (GameObject uk in UKObjects)
            {
                uk.SetActive(false);
            }
          */
            foreach (GameObject ita in ITAObjects)
            {
                ita.SetActive(false);
            }
            foreach (GameObject uk in UKObjects)
            {
                uk.SetActive(true);
            }
        }
    }

}
