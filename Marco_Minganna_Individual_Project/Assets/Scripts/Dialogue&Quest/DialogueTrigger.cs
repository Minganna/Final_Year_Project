using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Dialogue
{
    /// <summary>
    /// Class used to invoke a exit or enter action of a dialogue node
    /// </summary>
    public class DialogueTrigger : MonoBehaviour
    {
        /// <summary>
        /// Variable that need to match the nomenclature of the action in the dialogue node
        /// </summary>
        [SerializeField] string action;
        /// <summary>
        /// the event that needs to be trigger if called
        /// </summary>
        [SerializeField] UnityEvent onTrigger;
        /// <summary>
        /// Function called when an action is called
        /// </summary>
        /// <param name="actionToTrigger"></param>
        public void Trigger(string actionToTrigger)
        {
            if(actionToTrigger==action)
            {
                onTrigger.Invoke();
            }
        }
    }
}
