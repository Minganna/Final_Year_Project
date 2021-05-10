using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// class used to change the lighting in the menu in regards of the hour
/// </summary>
public class MenuGetTime : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vector2 hourminutes=new Vector2(0.0f,0.0f);
        DayAndNight Timechecker=new DayAndNight();
        hourminutes= Timechecker.GetDateandTime(hourminutes);
        GameObject sun = GameObject.FindGameObjectWithTag("Sun");

        
        if (hourminutes.x < 21 && hourminutes.x >= 8)
        {
            sun.SetActive(true);
        }
        else if (hourminutes.x >= 21 || hourminutes.x < 8)
        {
            sun.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
