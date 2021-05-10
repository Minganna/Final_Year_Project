using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// function used to animate the car wheels in the loading screen 
/// </summary>
public class CarWheel : MonoBehaviour
{
    [SerializeField]
    ///the list contains the 4 wheels of the car
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
