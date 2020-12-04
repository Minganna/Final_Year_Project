using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerInputs : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;

            if (Physics.Raycast(ray, out raycastHit, 100))
            {
                CommonVariables cv = this.GetComponent<QuestionsAndAnswers>().GetCV();
                // whatever tag you are looking for on your game object
                Debug.Log("Something Hit");
                if (raycastHit.collider.name == "Sphere")
                {

                    if (cv.UICanvas != null && !cv.UICanvas.activeSelf)
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
}
