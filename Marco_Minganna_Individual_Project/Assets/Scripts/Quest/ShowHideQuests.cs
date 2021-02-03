using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHideQuests : MonoBehaviour
{
    [SerializeField] GameObject QuestList;
    // Start is called before the first frame update
    void Start()
    {
        QuestList.SetActive(false);
    }

    public void ChangeQuestStatus()
    {
        QuestList.SetActive(!QuestList.activeSelf);
    }
}
