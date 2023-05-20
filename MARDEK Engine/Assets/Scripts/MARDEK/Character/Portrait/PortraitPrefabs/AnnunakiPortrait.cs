using UnityEngine;
using System.Collections.Generic;

namespace MARDEK.CharacterSystem
{
    public class AnnunakiPortrait : MonoBehaviour, IPortrait
    {
        [field: SerializeField] public PortraitType PortraitType { get; private set; }

        [SerializeField] SpriteRenderer face;
        [SerializeField] SpriteRenderer eye;
        [SerializeField] SpriteRenderer torso;

        [SerializeField] List<ExpressionSprite> expressions;

        public void SetPortrait(CharacterPortrait portrait)
        {
            face.sprite = portrait.Face;
            eye.sprite = portrait.Eye;
            torso.sprite = portrait.Torso;

            eye.transform.localPosition = new Vector3(-45, 0, 0);

            foreach(var expr in expressions)
            {
                expr.gameObject.SetActive(false);
            }
        }

        public void SetExpression(PortraitExpression expression)
        {
            foreach(var expr in expressions)
            {
                if (expr.expression == expression)
                {
                    expr.gameObject.SetActive(true);
                }
                else
                {
                    expr.gameObject.SetActive(false);
                }
            }

            switch (expression.name)
            {
                case "angr":
                    eye.transform.localPosition = new Vector3(-45, 15, 0);
                    break;

                case "grin":
                    eye.transform.localPosition = new Vector3(-45, 15, 0);
                    break;

                case "dreamy":
                    eye.transform.localPosition = new Vector3(-50, 50, 0);
                    break;

                case "susp":
                    eye.transform.localPosition = new Vector3(-45, -20, 0);
                    break;

                case "deep":
                    eye.transform.localPosition = new Vector3(-55, -25, 0);
                    break;

                default:
                    eye.transform.localPosition = new Vector3(-45, 0, 0);
                    break;
            }

        }
    }
}
