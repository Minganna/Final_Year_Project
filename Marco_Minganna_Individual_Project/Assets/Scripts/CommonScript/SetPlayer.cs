using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayer : MonoBehaviour
{
    CommonVariables cv = new CommonVariables();

    [SerializeField] GameObject Boy;
    [SerializeField] GameObject Girl;

    // Start is called before the first frame update
    void Start()
    {
        if(cv.getGender()=="Male")
        {
            Boy.SetActive(true);
        }
        else
        {
            Girl.SetActive(true);
        }
    }

}
