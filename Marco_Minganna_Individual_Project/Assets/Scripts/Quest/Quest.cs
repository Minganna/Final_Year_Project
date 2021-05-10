using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Namespace used to include all the script related to the QuestUI
/// </summary>
namespace QuestUI
{
    [CreateAssetMenu(fileName = "Quest", menuName = "Quest", order = 0)]
    public class Quest : ScriptableObject
    {
        /// <summary>
        /// The Objectives necessary to complete the quest
        /// </summary>
        [SerializeField] List<Objectives> objectives = new List<Objectives>();
        /// <summary>
        /// the rewards given by completing the quest
        /// </summary>
        [SerializeField] List<Rewards> rewards = new List<Rewards>();


        [System.Serializable]
        ///this class is used to show the type of reward the player gets by completing the quest
        public class Rewards
        {
            [Min(1)]
            public int number;
            public string item;
        }

        [System.Serializable]
        /// class used to keep track of the objective needed to complete the quest
       public  class Objectives
        {
            public string reference;
            public string description;
        }

        /// <summary>
        /// getter for the quest title
        /// </summary>
        /// <returns></returns>
        public string GetTitle()
        {
            return name;
        }
        /// <summary>
        /// getter for the number of objectives
        /// </summary>
        /// <returns></returns>
        public int GetObjectiveCount()
        {
            return objectives.Count;
        }

        /// <summary>
        /// return all the quest objectives
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Objectives> getObjectives()
        {
            return objectives;
        }


    /// <summary>
    /// return the quest rewards
    /// </summary>
    /// <returns></returns>
        public IEnumerable<Rewards> GetRewards()
        {
            return rewards;
        }

        /// <summary>
        /// checker used if the quest has a specific objective
        /// </summary>
        /// <param name="objectiveRef"></param>
        /// <returns></returns>

        internal bool HasObjective(string objectiveRef)
        {
            foreach(var objective in objectives)
            {
                if(objective.reference==objectiveRef)
                {
                    return true;
                }

            }
            return false;
        }

        /// <summary>
        /// used to find a quest by name
        /// </summary>
        /// <param name="questName"></param>
        /// <returns></returns>
        public static Quest GetByName(string questName)
        {
            foreach (Quest quest in Resources.LoadAll<Quest>(""))
            {
                if (quest.name == questName)
                {
                    return quest;
                }
            }
            return null;
        }
    }
}

