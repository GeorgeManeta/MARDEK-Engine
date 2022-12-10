using MARDEK.Inventory;
using MARDEK.Save;
using UnityEngine;

namespace MARDEK.Event
{
    public class ObtainPlotItemCommand : CommandBase
    {
        [SerializeField] GeneralProgressData generalProgressData;
        [SerializeField] PlotItem item;

        public override void Trigger()
        {
            generalProgressData.obtainedPlotItems.Add(item);
        }
    }
}
