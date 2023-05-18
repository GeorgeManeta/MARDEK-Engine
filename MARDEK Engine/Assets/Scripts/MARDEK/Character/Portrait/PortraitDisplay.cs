using UnityEngine;

namespace MARDEK.CharacterSystem
{
    public class PortraitDisplay : MonoBehaviour
    {
        [SerializeField] AnnunakiPortrait annunaki;
        [SerializeField] HumanPortrait humanF;
        [SerializeField] HumanPortrait humanM;
        [SerializeField] HumanPortrait humanChild;
        [SerializeField] RobotPortrait robot;
        [SerializeField] AruanPortrait aruan;
        [SerializeField] ReptoidPortrait reptoid;

        public void SetPortrait(CharacterPortrait portrait)
        {
            this.annunaki.gameObject.SetActive(false);
            this.humanF.gameObject.SetActive(false);
            this.humanM.gameObject.SetActive(false);
            this.humanChild.gameObject.SetActive(false);
            this.robot.gameObject.SetActive(false);
            this.aruan.gameObject.SetActive(false);
            this.reptoid.gameObject.SetActive(false);

            if (portrait != null)
            {
                this.gameObject.SetActive(true);

                switch (portrait.Type)
                {
                    case PortraitType.annunaki:
                        this.annunaki.SetPortrait(portrait);
                        this.annunaki.gameObject.SetActive(true);
                        break;

                    case PortraitType.humanF:
                        this.humanF.SetPortrait(portrait);
                        this.humanF.gameObject.SetActive(true);
                        break;

                    case PortraitType.humanM:
                        this.humanM.SetPortrait(portrait);
                        this.humanM.gameObject.SetActive(true);
                        break;

                    case PortraitType.humanChild:
                        this.humanChild.SetPortrait(portrait);
                        this.humanChild.gameObject.SetActive(true);
                        break;

                    case PortraitType.robot:
                        this.robot.SetPortrait(portrait);
                        this.robot.gameObject.SetActive(true);
                        break;

                    case PortraitType.aruan:
                        this.aruan.SetPortrait(portrait);
                        this.aruan.gameObject.SetActive(true);
                        break;

                    case PortraitType.reptoid:
                        this.reptoid.SetPortrait(portrait);
                        this.reptoid.gameObject.SetActive(true);
                        break;
                }
            }
            else
            {
                this.gameObject.SetActive(false);
            }
        }
    }
}