using MARDEK.Inventory;

namespace MARDEK.UI
{
    public abstract class BattleLootSelectable : Selectable
    {
        public static BattleLootSelectable currentlySelected;

        public abstract void Interact(Item[] items, int[] amounts);

        override public void Select(bool playSFX = true)
        {
            base.Select(playSFX);
            currentlySelected = this;
        }
    }
}
