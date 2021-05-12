using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;


namespace QuestUI
{
    /// <summary>
    /// class that keep track of the quests
    /// </summary>
    public class QuestList : MonoBehaviour,IpredicateEvaluator
    {
        /// <summary>
        /// referece to each quest status
        /// </summary>
        static List<QuestStatus> statuses=new List<QuestStatus>();
        public event Action onUpdate;
        
       
        /// <summary>
        /// add a quest to the list of quests if the player doesn't already have it
        /// </summary>
        /// <param name="quest"></param>
        public void AddQuest(Quest quest)
        {
            if (HasQuest(quest)) return;
            QuestStatus newStatus= new QuestStatus(quest);
            statuses.Add(newStatus);
            if(onUpdate!=null)
            {
                onUpdate();
            }
        }

        /// <summary>
        /// used to complete the objective found by name and give the reward if all objective of a quest are completed
        /// </summary>
        /// <param name="quest"></param>
        /// <param name="objective"></param>
        public void CompleteObjective(Quest quest, string objective)
        {
            QuestStatus status= GetQuestStatus(quest);
            status.CompleteObjective(objective);
            if(status.isComplete())
            {
                GiveRewards(quest);
            }
            if (onUpdate != null)
            {
                onUpdate();
            }
        }

 
        /// <summary>
        /// used to check if the player has a specific quest
        /// </summary>
        /// <param name="quest"></param>
        /// <returns></returns>
        public bool HasQuest(Quest quest)
        {
            return GetQuestStatus(quest)!=null;
        }
        /// <summary>
        /// getter for all the quests status 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<QuestStatus> GetStatuses()
        {
            return statuses;
        }

        /// <summary>
        /// get a specific quest status
        /// </summary>
        /// <param name="quest"></param>
        /// <returns></returns>
        private QuestStatus GetQuestStatus(Quest quest)
        {
            foreach (QuestStatus status in statuses)
            {
                string debug = status.GetQuest().name + statuses.Count;
                Debug.Log(debug);
               
                if (status.GetQuest() == quest)
                {
                    Debug.Log("Found quest" + quest.name);
                    return status;
                }
            }
            return null;
        }

        /// <summary>
        /// function used to give the player the reward when a quest is completed
        /// </summary>
        /// <param name="quest"></param>
        private void GiveRewards(Quest quest)
        {
            foreach(var reward in quest.GetRewards())
            {
                if(reward.item=="gold")
                {
                    this.GetComponent<Wallet>().addCoinsToWallet(reward.number);
                }
                
            }
        }

        /// <summary>
        /// function used to check if the player possess or has completed a quest
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public bool? Evaluate(string predicate, string[] parameters)
        {
            switch(predicate)
            {
                case "HasQuest":
                    return HasQuest(Quest.GetByName(parameters[0]));
                case "CompletedQuest":
                    return GetQuestStatus(Quest.GetByName(parameters[0])).isComplete();
            }

            return null;
        }
    }

}