using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestUI
{
    [CreateAssetMenu(fileName = "Quest", menuName = "Quest", order = 0)]
    public class Quest : ScriptableObject
    {
        [SerializeField] List<Objectives> objectives = new List<Objectives>();
        [SerializeField] List<Rewards> rewards = new List<Rewards>();


        [System.Serializable]
        public class Rewards
        {
            [Min(1)]
            public int number;
            public string item;
        }

        [System.Serializable]
       public  class Objectives
        {
            public string reference;
            public string description;
        }

        public string GetTitle()
        {
            return name;
        }
        public int GetObjectiveCount()
        {
            return objectives.Count;
        }
        public IEnumerable<Objectives> getObjectives()
        {
            return objectives;
        }

        public IEnumerable<Rewards> GetRewards()
        {
            return rewards;
        }

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

