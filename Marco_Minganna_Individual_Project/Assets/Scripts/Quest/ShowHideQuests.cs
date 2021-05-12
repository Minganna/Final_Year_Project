using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// class used to show and hide the quest UI
/// </summary>
public class ShowHideQuests : MonoBehaviour
{
    [SerializeField] GameObject QuestList;
    // Start is called before the first frame update
    void Start()
    {
        QuestList.SetActive(false);
    }

    /// <summary>
    /// shows and hide the quest UI
    /// </summary>
    public void ChangeQuestStatus()
    {
        QuestList.SetActive(!QuestList.activeSelf);
    }
}
