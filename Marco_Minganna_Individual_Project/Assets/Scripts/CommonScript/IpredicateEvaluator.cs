using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public interface IpredicateEvaluator 
    {
        bool? Evaluate(string predicate, string[] parameters);
    }
}
