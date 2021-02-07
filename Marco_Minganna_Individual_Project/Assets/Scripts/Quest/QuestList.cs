using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;


namespace QuestUI
{
    public class QuestList : MonoBehaviour,IpredicateEvaluator
    {
        List<QuestStatus> statuses=new List<QuestStatus>();
        public event Action onUpdate;
        
       

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

 

        public bool HasQuest(Quest quest)
        {
            return GetQuestStatus(quest)!=null;
        }
        public IEnumerable<QuestStatus> GetStatuses()
        {
            return statuses;
        }

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