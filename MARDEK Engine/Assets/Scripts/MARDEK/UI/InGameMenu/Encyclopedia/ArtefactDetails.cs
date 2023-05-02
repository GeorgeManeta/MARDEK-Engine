using MARDEK.Progress;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace MARDEK.UI
{
    public class ArtefactDetails : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI title;
        [SerializeField] Image image;
        [SerializeField] TextMeshProUGUI description;

        public void SetArtefact(EncyclopediaArtefact artefact)
        {
            title.text = artefact.displayName;
            image.sprite = artefact.image;
            description.text = artefact.description;
        }
    }
}
