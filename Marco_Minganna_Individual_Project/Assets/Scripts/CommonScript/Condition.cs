using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    [System.Serializable]
    public class Condition
    {
        [SerializeField]
        Disjunction[] and;

        public bool check(IEnumerable<IpredicateEvaluator> evaluators)
        {
            foreach(Disjunction dis in and)
            {
                if(!dis.check(evaluators))
                {
                    return false;
                }
            }
            return true;
        }

            [System.Serializable]
        class Disjunction
        {
            [SerializeField]
            Predicate[] or;
            public bool check(IEnumerable<IpredicateEvaluator> evaluators)
            {
                foreach (Predicate pred in or)
                {
                    if (pred.check(evaluators))
                    {
                        return true;
                    }
                }
                return false;
            }


         }

        [System.Serializable]
        class Predicate
        {
            [SerializeField]
            string predicate;
            [SerializeField]
            string[] parameters;
            [SerializeField]
            bool negate = false;

            public bool check(IEnumerable<IpredicateEvaluator> evaluators)
            {
                foreach (var evaluator in evaluators)
                {
                    bool? result = evaluator.Evaluate(predicate, parameters);
                    if (result == null)
                    {
                        continue;
                    }
                    if (result == negate)
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        

    }
}
