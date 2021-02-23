using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveBetweenWorlds : MonoBehaviour
{
    [SerializeField] GameObject NextArea;
    [SerializeField] GameObject ThisArea;
    [SerializeField] GameObject PreviousArea;

    [SerializeField] 
    static bool nextAreaUnlocked=false;

    [SerializeField] int NextAreaSceneN;


    public void SetAreaUnlock()
    {
        nextAreaUnlocked = true;
    }
    public void MoveToNextArea()
    {

        if(nextAreaUnlocked&&ThisArea.activeSelf)
        {
            ThisArea.SetActive(false);
            NextArea.SetActive(true);
            return;
        }
        if(PreviousArea.activeSelf)
        {
            PreviousArea.SetActive(false);
            ThisArea.SetActive(true);
            return;
        }
    }

    public void MoveToPreviousArea()
    {
        if(NextArea.activeSelf)
        {
            NextArea.SetActive(false);
            ThisArea.SetActive(true);
            return;
        }
        if(ThisArea.activeSelf)
        {
            if(PreviousArea)
            {
                ThisArea.SetActive(false);
                PreviousArea.SetActive(true);
            }
            return;
        }
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(NextAreaSceneN);
    }
}
