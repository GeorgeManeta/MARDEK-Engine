using MARDEK.Progress;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace MARDEK.UI
{
    public class MonsterDetails : MonoBehaviour
    {
        [SerializeField] Image elementIcon;
        [SerializeField] TextMeshProUGUI nameText;
        [SerializeField] TextMeshProUGUI descriptionText;

        [SerializeField] TextMeshProUGUI classValue;

        public void SetMonster(EncyclopediaMonster monster)
        {
            elementIcon.sprite = monster.element.thickSprite;
            nameText.text = monster.displayName;
            descriptionText.text = monster.description;
            classValue.text = monster.battleClass;
        }
    }
}
