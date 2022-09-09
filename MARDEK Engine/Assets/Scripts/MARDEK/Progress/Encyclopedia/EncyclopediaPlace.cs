using UnityEngine;

namespace MARDEK.Progress
{
    [CreateAssetMenu(menuName = "MARDEK/Encyclopedia/Place")]
    public class EncyclopediaPlace : EncyclopediaItem
    {
        [SerializeField] Sprite _landscape;
        [SerializeField] string _description;

        public Sprite landscape { get { return _landscape; } }
        public string description { get { return _description; } }
    }
}