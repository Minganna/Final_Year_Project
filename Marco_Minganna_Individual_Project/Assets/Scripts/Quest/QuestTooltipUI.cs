using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

namespace QuestUI
{
    public class QuestTooltipUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI title;
        [SerializeField] Transform objectiveContainer;
        [SerializeField] GameObject objectivePrefab;
        [SerializeField] GameObject objectiveIncompletePrefab;
        [SerializeField] TextMeshProUGUI rewardText;
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
