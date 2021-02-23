using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestUI
{
    public class QuestStatus 
    {
         Quest quest;
          static List<string> completedObjectives=new List<string>();

        public QuestStatus(Quest quest)
        {
            this.quest = quest;
        }

        public bool isComplete()
        {
            foreach(var objective in quest.getObjectives())
            {
                if(!completedObjectives.Contains(objective.reference))
                {
                    return false;
                }
            }
            return true;
        }

        public Quest GetQuest()
        {
            return quest;
        }

        public int GetCompletedCount()
        {
            return completedObjectives.Count;
        }

        public bool isObjectiveCompleted(string objective)
        {
            return completedObjectives.Contains(objective);
        }

        public void CompleteObjective(string objective)
        {
            if(quest.HasObjective(objective))
            {
                completedObjectives.Add(objective);
            }
           
        }
    }

}
