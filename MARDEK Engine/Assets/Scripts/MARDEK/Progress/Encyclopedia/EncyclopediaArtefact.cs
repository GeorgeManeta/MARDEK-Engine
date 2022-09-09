using UnityEngine;

namespace MARDEK.Progress
{
    [CreateAssetMenu(menuName = "MARDEK/Encyclopedia/Artefact")]
    public class EncyclopediaArtefact : EncyclopediaItem
    {
        [SerializeField] Sprite _image;
        [SerializeField] string _description;

        public Sprite image { get { return _image; } }
        public string description { get { return _description; } }
    }
}