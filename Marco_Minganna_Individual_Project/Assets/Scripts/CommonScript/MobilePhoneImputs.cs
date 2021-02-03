using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_IOS
public class MobilePhoneImputs: MonoBehaviour
{
  
    private void Update()
    {
     
        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit raycastHit;
            if (Physics.Raycast(raycast, out raycastHit))
            {
                CommonVariables cv = this.GetComponent<QuestionsAndAnswers>().GetCV();
                Debug.Log("Something Hit");
                if (raycastHit.collider.name == "Sphere")
                {
                    
                    if(cv.UICanvas!=null&&!cv.UICanvas.activeSelf)
                    {
                         cv.UICanvas.SetActive(true);
                    }
                    return;
                }

                //OR with Tag

                if (raycastHit.collider.CompareTag("Sun"))
                {
                    if (cv.UICanvas != null && !cv.UICanvas.activeSelf)
                    {
                        cv.UICanvas.SetActive(true);
                    }
                    return;
                }
            }
        }
    }
    public void openPhoneKeyboard()
    {
       
        Debug.Log("iphone connected");
        TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false, false, true);
       
    }


}
#endif