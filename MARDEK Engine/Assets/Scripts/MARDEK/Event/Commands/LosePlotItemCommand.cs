using MARDEK.Inventory;
using MARDEK.Save;
using UnityEngine;

namespace MARDEK.Event
{
    public class LosePlotItemCommand : CommandBase
    {
        [SerializeField] GeneralProgressData generalProgressData;
        [SerializeField] PlotItem item;

        public override void Trigger()
        {
            generalProgressData.obtainedPlotItems.Remove(item);
        }
    }
}
