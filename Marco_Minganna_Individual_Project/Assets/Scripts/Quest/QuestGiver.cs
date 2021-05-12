using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestUI
{
    /// <summary>
    /// class used to allow an NPC to assign a quest to the player
    /// </summary>
    public class QuestGiver : MonoBehaviour
    {
        /// <summary>
        /// a reference to the quest to assign
        /// </summary>
        [SerializeField] Quest quest;

        /// <summary>
        /// the function used to assign the quest to the player
        /// </summary>
        public void GiveQuest()
        {
            QuestList questList = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestList>();
            questList.AddQuest(quest);
        }

    }

}
