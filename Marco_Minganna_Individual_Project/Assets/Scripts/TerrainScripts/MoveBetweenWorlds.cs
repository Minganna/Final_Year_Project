using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// class used to move the player between the scenes
/// </summary>
public class MoveBetweenWorlds : MonoBehaviour
{
    /// <summary>
    /// variable used to visualised the miniature of the next area in the boxworld
    /// </summary>
    [SerializeField] GameObject NextArea;
    /// <summary>
    /// variable used to visualised the miniature of the current area in the boxworld
    /// </summary>
    [SerializeField] GameObject ThisArea;
    /// <summary>
    /// variable used to visualised the miniature of the hotel area in the boxworld
    /// </summary>
    [SerializeField] GameObject PreviousArea;
    [SerializeField] GameObject[] Movebutton;
    CommonVariables cv = new CommonVariables();

    [SerializeField] 
    static bool nextAreaUnlocked=false;

    [SerializeField] int NextAreaSceneN;
    [SerializeField] int PreviousAreaSceneN;

    int MoveTo;

    /// <summary>
    /// locker used to confirm that the player followed the tutorial for moving between areas
    /// </summary>
    public void SetAreaUnlock()
    {
        nextAreaUnlocked = true;
    }
    /// <summary>
    /// the logic used to move to the next area
    /// </summary>
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
    /// <summary>
    /// logic used to allow the player to move to the hotel 
    /// </summary>
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
    /// <summary>
    /// function used to change scene
    /// </summary>
    public void ChangeScene()
    {
        cv.setTransp("train");
        cv.setSceneToLoad(MoveTo);
        SceneManager.LoadScene(2);
    }
}
