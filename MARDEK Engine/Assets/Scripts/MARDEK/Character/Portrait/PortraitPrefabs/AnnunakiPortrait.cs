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

        [SerializeField] List<PortraitExpression> expressions;

        public void SetPortrait(CharacterPortrait portrait)
        {
            face.sprite = portrait.Face;
            eye.sprite = portrait.Eye;
            torso.sprite = portrait.Torso;

            // apply norm expression
            expressions[0].ApplyTransforms();
        }

        public void SetExpression(PortraitExpressionEnum expression)
        {
            bool match = false;
            foreach(var expr in expressions)
            {
                if (expr.Expression == expression)
                {
                    expr.gameObject.SetActive(true);
                    expr.ApplyTransforms();
                    match = true;
                }
                else
                {
                    expr.gameObject.SetActive(false);
                }
            }
            if (!match)
            {
                // apply norm expression
                expressions[0].gameObject.SetActive(true);
                expressions[0].ApplyTransforms();
            }

            /*
            foreach(var expr in expressions)
            {
                if (expr.Expression == Expression)
                {
                    expr.gameObject.SetActive(true);
                }
                else
                {
                    expr.gameObject.SetActive(false);
                }
            }


            switch (Expression)
            {
                case var v when v == this.map.GetExp("angr"):
                    eye.transform.localPosition = new Vector3(-45, 15, 0);
                    break;

                case var v when v == this.map.GetExp("grin"):
                    eye.transform.localPosition = new Vector3(-45, 15, 0);
                    break;

                case var v when v == this.map.GetExp("dreamy"):
                    eye.transform.localPosition = new Vector3(-50, 50, 0);
                    break;

                case var v when v == this.map.GetExp("susp"):
                    eye.transform.localPosition = new Vector3(-45, -20, 0);
                    break;

                case var v when v == this.map.GetExp("deep"):
                    eye.transform.localPosition = new Vector3(-55, -25, 0);
                    break;

                default:
                    eye.transform.localPosition = new Vector3(-45, 0, 0);
                    break;
            }
            */
        }
    }
}
