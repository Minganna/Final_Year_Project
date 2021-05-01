using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveBetweenWorlds : MonoBehaviour
{
    [SerializeField] GameObject NextArea;
    [SerializeField] GameObject ThisArea;
    [SerializeField] GameObject PreviousArea;
    [SerializeField] GameObject[] Movebutton;
    CommonVariables cv = new CommonVariables();

    [SerializeField] 
    static bool nextAreaUnlocked=true;

    [SerializeField] int NextAreaSceneN;
    [SerializeField] int PreviousAreaSceneN;

    int MoveTo;


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
            PreviousArea.SetActive(false);
            Movebutton[1].SetActive(true);
            NextWorld(NextAreaSceneN);
            return;
        }
        if(PreviousArea.activeSelf)
        {
            PreviousArea.SetActive(false);
            ThisArea.SetActive(true);
            Movebutton[0].SetActive(false);
            return;
        }
    }

    public void MoveToPreviousArea()
    {
 
        if(ThisArea.activeSelf)
        {
                NextArea.SetActive(false);
                ThisArea.SetActive(false);
                PreviousArea.SetActive(true);
                Movebutton[0].SetActive(true);
                NextWorld(PreviousAreaSceneN);
            return;
        }
        if (NextArea.activeSelf)
        {
            NextArea.SetActive(false);
            ThisArea.SetActive(true);
            Movebutton[1].SetActive(false);
            return;
        }
    }

    public void NextWorld(int next)
    {
        MoveTo = next;
    }
    public void ChangeScene()
    {
        cv.setTransp("train");
        cv.setSceneToLoad(MoveTo);
        SceneManager.LoadScene(2);
    }
}
