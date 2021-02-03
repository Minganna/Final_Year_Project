using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuestUI;

public class QuestListUI : MonoBehaviour
{
    [SerializeField] Quest[] tempQuests;
    [SerializeField] QuestItemUI questPrefab;
    // Start is called before the first frame update
    void Start()
    {
        QuestItemUI[] children = gameObject.GetComponentsInChildren<QuestItemUI>();
        foreach (QuestItemUI child in children)
        {
            Destroy(child.gameObject);
        }
        foreach(Quest quest in tempQuests)
        {
            QuestItemUI uiInstance= Instantiate<QuestItemUI>(questPrefab, transform);
            uiInstance.SetUp(quest);
        }
    }

}
