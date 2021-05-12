using System.Collections;
using System.Collections.Generic;
using UI.Tooltips;
using UnityEngine;

namespace QuestUI
{
    /// <summary>
    /// class used to control the quest tooltip
    /// </summary>
    public class QuestTooltip : TooltipSpawner
    {
        /// <summary>
        /// used to determine when the tooltip can be spawned
        /// </summary>
        /// <returns></returns>
        public override bool CanCreateTooltip()
        {
            return true;
        }

        /// <summary>
        /// used to update the informations in the tooltip
        /// </summary>
        /// <param name="tooltip"></param>
        public override void UpdateTooltip(GameObject tooltip)
        {
            QuestStatus status=GetComponent<QuestItemUI>().GetQuestStatus();
            tooltip.GetComponent<QuestTooltipUI>().SetUp(status);
        }
    }
}

