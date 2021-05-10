using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// class used to keep track of the device clock time
/// </summary>
public class DayAndNight 
{
    /// <summary>
    /// function used to return the device time
    /// </summary>
    /// <param name="checktime"></param>
    /// <returns></returns>
   public Vector2 GetDateandTime(Vector2 checktime)
    {
        var time = System.DateTime.Now;
        checktime.x = time.Hour;
        checktime.y = time.Minute;
        return checktime;
    }

    
}
