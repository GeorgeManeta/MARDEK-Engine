using MARDEK.Progress;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace MARDEK.UI
{
    public class PersonDetails : MonoBehaviour
    {
        [SerializeField] Image elementIcon;
        [SerializeField] TextMeshProUGUI fullNameText;
        [SerializeField] TextMeshProUGUI descriptionText;

        [SerializeField] Image portrait;
        [SerializeField] TextMeshProUGUI raceValue;
        [SerializeField] TextMeshProUGUI genderValue;
        [SerializeField] TextMeshProUGUI ageValue;
        [SerializeField] TextMeshProUGUI classValue;
        [SerializeField] TextMeshProUGUI elementValue;
        [SerializeField] TextMeshProUGUI placeOfOriginValue;
        [SerializeField] TextMeshProUGUI weaponValue;
        [SerializeField] TextMeshProUGUI alignmentValue;

        public void SetPerson(EncyclopediaPerson person)
        {
            elementIcon.sprite = person.element.thickSprite;
            fullNameText.text = person.fullName;
            descriptionText.text = person.fullDescription;

            portrait.sprite = person.portrait;
            raceValue.text = person.race;
            genderValue.text = person.gender;
            ageValue.text = person.age.ToString();
            classValue.text = person.battleClass;
            elementValue.text = person.element.name.ToUpper();
            placeOfOriginValue.text = person.placeOfOrigin;
            weaponValue.text = person.weapon;
            alignmentValue.text = person.alignment;
        }
    }
}
