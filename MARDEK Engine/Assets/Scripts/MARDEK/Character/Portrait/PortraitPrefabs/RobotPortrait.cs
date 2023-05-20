using UnityEngine;

namespace MARDEK.CharacterSystem
{
    public class RobotPortrait : PortraitPrefab
    {
        [field: SerializeField] public override PortraitType PortraitType { get; protected set; }

        // There is only one robot (Legion), so this is basically empty...

        public override void SetPortrait(CharacterPortrait portrait) { }
    }
}
