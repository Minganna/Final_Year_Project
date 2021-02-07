using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuestUI;

public class QuestListUI : MonoBehaviour
{
    [SerializeField] QuestItemUI questPrefab;
    QuestList questList;
    // Start is called before the first frame update
    void Start()
    {
        questList = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestList>();
        questList.onUpdate += Redraw;
        Redraw();
    }

    private void Redraw()
    {
        QuestItemUI[] children = gameObject.GetComponentsInChildren<QuestItemUI>();
        foreach (QuestItemUI child in children)
        {
            Destroy(child.gameObject);
        }
        foreach (QuestStatus status in questList.GetStatuses())
        {
            QuestItemUI uiInstance = Instantiate<QuestItemUI>(questPrefab, transform);
            uiInstance.SetUp(status);
        }
    }
}
