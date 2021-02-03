using System.Collections;
using System.Collections.Generic;
using UI.Tooltips;
using UnityEngine;

namespace QuestUI
{
    public class QuestTooltip : TooltipSpawner
    {
        public override bool CanCreateTooltip()
        {
            return true;
        }

        public override void UpdateTooltip(GameObject tooltip)
        {
        }
    }
}

