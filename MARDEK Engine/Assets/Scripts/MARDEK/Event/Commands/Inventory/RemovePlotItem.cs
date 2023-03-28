using UnityEngine;

namespace MARDEK.Event
{
    using Inventory;
    using CharacterSystem;
    public class RemovePlotItem : CommandBase
    {
        [SerializeField] PlotItem item;

        public override void Trigger()
        {
            Party.Instance.plotItems.Remove(item);
        }
    }
}