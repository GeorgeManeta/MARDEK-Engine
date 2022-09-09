using MARDEK.Progress;
using UnityEngine;
using UnityEngine.UI;

namespace MARDEK.UI
{
    public class PlaceDetails : MonoBehaviour
    {
        [SerializeField] Text title;
        [SerializeField] Image landscape;
        [SerializeField] Text description;

        public void SetPlace(EncyclopediaPlace place)
        {
            title.text = place.displayName;
            landscape.sprite = place.landscape;
            description.text = place.description;
        }
    }
}
