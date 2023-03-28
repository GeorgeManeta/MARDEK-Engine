using UnityEngine;

namespace MARDEK.Event
{
    using Inventory;
    using CharacterSystem;
    public class AddPlotItem : CommandBase
    {
        [SerializeField] PlotItem item;

        public override void Trigger()
        {
            Party.Instance.plotItems.Add(item);
        }
    }
}