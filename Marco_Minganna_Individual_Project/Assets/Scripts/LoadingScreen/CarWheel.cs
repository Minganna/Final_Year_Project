using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarWheel : MonoBehaviour
{
    [SerializeField]
    List<GameObject> carWheels = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject wheels in carWheels)
        {
            wheels.transform.Rotate(0, 0, 7.5f);
        }    
    }
}
