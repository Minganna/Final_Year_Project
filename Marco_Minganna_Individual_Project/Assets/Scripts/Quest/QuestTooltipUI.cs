using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

namespace QuestUI
{
    /// <summary>
    /// class used to organize the data in the tooltip
    /// </summary>
    public class QuestTooltipUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI title;
        [SerializeField] Transform objectiveContainer;
        [SerializeField] GameObject objectivePrefab;
        [SerializeField] GameObject objectiveIncompletePrefab;
        [SerializeField] TextMeshProUGUI rewardText;

        /// <summary>
        /// used to set up the information regarding the quest 
        /// </summary>
        /// <param name="status"></param>
        public void SetUp(QuestStatus status)
        {
            Quest quest = status.GetQuest();
            title.text = quest.GetTitle();
            QuestDummy[] children = objectiveContainer.GetComponentsInChildren<QuestDummy>();
            foreach (QuestDummy child in children)
            {
                Destroy(child.gameObject);
            }
            foreach(var objective in quest.getObjectives())
            {
                GameObject prefab = objectiveIncompletePrefab;
                if(status.isObjectiveCompleted(objective.reference))
                {
                    prefab = objectivePrefab;
                }
               GameObject objectiveInstance= Instantiate(prefab, objectiveContainer);
               TextMeshProUGUI objectiveText= objectiveInstance.GetComponentInChildren<TextMeshProUGUI>();
                objectiveText.text = objective.description;
            }

            rewardText.text = GetRewardText(quest);

        }

        /// <summary>
        /// used to update the reward text in the tooltip
        /// </summary>
        /// <param name="quest"></param>
        /// <returns></returns>
        private string GetRewardText(Quest quest)
        {
            string reward = "";
            foreach(var currentReward in quest.GetRewards())
            {
                if(reward!="")
                {
                    reward += ", ";
                }
                reward += currentReward.item + ": " + currentReward.number;
            }
            if(reward=="")
            {
                reward = "No reward";
            }
            reward += ".";
            return reward;
        }
    }
}
