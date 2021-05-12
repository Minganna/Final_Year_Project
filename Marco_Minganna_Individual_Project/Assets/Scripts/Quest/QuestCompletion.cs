using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestUI
{
    /// <summary>
    /// script used to mark as completed the objectives when the conditions are met
    /// </summary>
    public class QuestCompletion : MonoBehaviour
    {
        /// <summary>
        /// variable that stores the quest
        /// </summary>
        [SerializeField] Quest quest;
        /// <summary>
        /// variable that stores the objective
        /// </summary>
        [SerializeField] string objective;

        /// <summary>
        /// mark the objective as completed when needed
        /// </summary>
        public void CompleteObjective()
        {
            QuestList questList = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestList>();
            questList.CompleteObjective(quest, objective);
        }
    }

}