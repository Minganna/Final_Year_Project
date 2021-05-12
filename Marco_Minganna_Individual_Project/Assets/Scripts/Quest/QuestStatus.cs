using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestUI
{
    /// <summary>
    /// Class used to keep track of each quest status in completion
    /// </summary>
    public class QuestStatus 
    {
        /// <summary>
        /// stores the quest
        /// </summary>
         Quest quest;
        /// <summary>
        /// stores the list of objectives
        /// </summary>
          static List<string> completedObjectives=new List<string>();

        /// <summary>
        /// constructor of the class
        /// </summary>
        /// <param name="quest"></param>
        public QuestStatus(Quest quest)
        {
            this.quest = quest;
        }

        /// <summary>
        /// function used to check if a objective is completed
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// getter for the quest
        /// </summary>
        /// <returns></returns>
        public Quest GetQuest()
        {
            return quest;
        }
        /// <summary>
        /// getter for the number of objective completed
        /// </summary>
        /// <returns></returns>
        public int GetCompletedCount()
        {
            return completedObjectives.Count;
        }

        /// <summary>
        /// used to confirm if an objective is completed
        /// </summary>
        /// <param name="objective"></param>
        /// <returns></returns>
        public bool isObjectiveCompleted(string objective)
        {
            return completedObjectives.Contains(objective);
        }

        /// <summary>
        /// used to add the objective to the complete list
        /// </summary>
        /// <param name="objective"></param>
        public void CompleteObjective(string objective)
        {
            if(quest.HasObjective(objective))
            {
                completedObjectives.Add(objective);
            }
           
        }
    }

}
