using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Class used to modify if needed the gameobjects that should only appear in England/Italy
/// </summary>
public class UKITAchanges : MonoBehaviour
{
    /// <summary>
    /// The script that keep track of the common static strings
    /// </summary>
    CommonVariables cv = new CommonVariables();
    [SerializeField]
    ///List of uk specific objects
    List<GameObject> UKObjects = new List<GameObject>();
    [SerializeField]
    ///list of italian objects
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
