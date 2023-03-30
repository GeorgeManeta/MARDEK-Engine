using UnityEngine;

namespace MARDEK.Progress
{
    using Event;
    using Inventory;
    public class RemovePlotItem : CommandBase
    {
        [SerializeField] PlotItem item;

        public override void Trigger()
        {
            Party.Instance.plotItems.Remove(item);
        }
    }
}