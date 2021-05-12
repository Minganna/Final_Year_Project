using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuestUI;
using TMPro;

/// <summary>
/// class used to show the quest in the UI
/// </summary>
public class QuestItemUI : MonoBehaviour
{
    /// <summary>
    /// stores the text ui field where the name of the quest will appear
    /// </summary>
    [SerializeField] TextMeshProUGUI title;
    /// <summary>
    /// stores the ui element where the progress will be shown
    /// </summary>
    [SerializeField] TextMeshProUGUI progress;
    /// <summary>
    /// link to the class that stores the status of the quests
    /// </summary>
    QuestStatus status;
    /// <summary>
    /// used to set up the UI
    /// </summary>
    /// <param name="status"></param>
    public void SetUp(QuestStatus status)
    {
        this.status = status;
        title.text = status.GetQuest().GetTitle();
        progress.text = status.GetCompletedCount()+"/" + status.GetQuest().GetObjectiveCount();
    }
    /// <summary>
    /// getter for this quest status
    /// </summary>
    /// <returns></returns>
    public QuestStatus GetQuestStatus()
    {
        return status;
    }
}
