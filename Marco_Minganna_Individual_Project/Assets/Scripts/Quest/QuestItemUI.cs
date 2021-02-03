using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuestUI;
using TMPro;

public class QuestItemUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI progress;
    public void SetUp(Quest quest)
    {
        title.text = quest.GetTitle();
        progress.text = "0/" + quest.GetObjectiveCount();
    }
}
