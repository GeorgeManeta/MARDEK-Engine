namespace MARDEK.UI
{
    using MARDEK.CharacterSystem;

    public class CharacterEquipmentUI : InventoryUI
    {
        public Character Character
        {
            get
            {
                var index = transform.GetSiblingIndex();
                if (Party.Instance.Characters.Count <= index)
                    return null;
                return Party.Instance.Characters[index];
            }
        }
        private void OnEnable()
        {
            var index = transform.GetSiblingIndex();
            if(Character != null)
                AssignInventoryToUI(Character.EquippedItems);
        }
    }
}