using MARDEK.Progress;
using UnityEngine;
using UnityEngine.UI;

namespace MARDEK.UI
{
    public class ArtefactDetails : MonoBehaviour
    {
        [SerializeField] Text title;
        [SerializeField] Image image;
        [SerializeField] Text description;

        public void SetArtefact(EncyclopediaArtefact artefact)
        {
            title.text = artefact.displayName;
            image.sprite = artefact.image;
            description.text = artefact.description;
        }
    }
}
