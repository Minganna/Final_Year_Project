using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayAndNight 
{

   public Vector2 GetDateandTime(Vector2 checktime)
    {
        var time = System.DateTime.Now;
        checktime.x = time.Hour;
        checktime.y = time.Minute;
        return checktime;
    }

    
}
