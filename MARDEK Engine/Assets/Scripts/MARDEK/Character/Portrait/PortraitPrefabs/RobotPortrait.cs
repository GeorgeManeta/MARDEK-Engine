using UnityEngine;

namespace MARDEK.CharacterSystem
{
    public class RobotPortrait : MonoBehaviour, IPortrait
    {
        [field: SerializeField] public PortraitType PortraitType { get; private set; }

        // There is only one robot (Legion), so this is basically empty...
        // All the robot assets are present in the RobotPortrait prefab.

        public void SetPortrait(CharacterPortrait portrait) { }

        public void SetExpression(PortraitExpressionEnum expression)
        {

        }
    }
}
