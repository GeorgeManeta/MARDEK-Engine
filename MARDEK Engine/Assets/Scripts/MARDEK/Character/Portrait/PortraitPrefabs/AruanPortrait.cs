using System.Collections.Generic;
using UnityEngine;

namespace MARDEK.CharacterSystem
{
    public class AruanPortrait : MonoBehaviour, IPortrait
    {
        [SerializeField] StringExpressionMap map;

        [field: SerializeField] public PortraitType PortraitType { get; private set; }

        [SerializeField] SpriteRenderer face;
        [SerializeField] SpriteRenderer leftEye;
        [SerializeField] SpriteRenderer leftBrow;
        [SerializeField] SpriteRenderer rightEye;
        [SerializeField] SpriteRenderer rightBrow;

        [SerializeField] SpriteRenderer mouth;

        [SerializeField] SpriteRenderer torso;

        [SerializeField] List<ExpressionSprite> expressions;

        public void SetPortrait(CharacterPortrait portrait)
        {
            face.sprite = portrait.Face;
            leftEye.sprite = portrait.Eye;
            rightEye.sprite = portrait.Eye;
            leftBrow.sprite = portrait.Eyebrow;
            rightBrow.sprite = portrait.Eyebrow;
            torso.sprite = portrait.Torso;
            mouth.sprite = portrait.Mouth;

            leftBrow.transform.localPosition = new Vector3(-45, 38, 0);
            leftBrow.transform.localRotation = Quaternion.Euler(0, 0, -10);
            rightBrow.transform.localPosition = new Vector3(-165, 50, 0);
            rightBrow.transform.localRotation = Quaternion.Euler(0, 0, 0);

            foreach(var expr in expressions) { 

                expr.gameObject.SetActive(false);
            }

            // both eyes norm outline
            expressions[0].gameObject.SetActive(true);
            expressions[1].gameObject.SetActive(true);
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

            switch (expression)
            {
                case var v when v == map.GetExp("susp"):
                    rightBrow.transform.localPosition = new Vector3(-170, 35, 0);
                    rightBrow.transform.localRotation = Quaternion.Euler(0, 0, 20);
                    leftBrow.transform.localPosition = new Vector3(-45, 50, 0);
                    leftBrow.transform.localRotation = Quaternion.Euler(0, 0, -20);

                    // right eye deep outline
                    expressions[3].gameObject.SetActive(true);

                    // left eye norm outline
                    expressions[0].gameObject.SetActive(true);

                    break;

                case var v when v == map.GetExp("deep"):
                    rightBrow.transform.localPosition = new Vector3(-170, 35, 0);
                    rightBrow.transform.localRotation = Quaternion.Euler(0, 0, 10);
                    leftBrow.transform.localPosition = new Vector3(-45, 25, 0);
                    leftBrow.transform.localRotation = Quaternion.Euler(0, 0, -10);

                    break;

                case var v when v == map.GetExp("angr"):
                    rightBrow.transform.localPosition = new Vector3(-165, 30, 0);
                    rightBrow.transform.localRotation = Quaternion.Euler(0, 0, 8);
                    leftBrow.transform.localPosition = new Vector3(-55, 27, 0);
                    leftBrow.transform.localRotation = Quaternion.Euler(0, 0, -5);
                    break;

                case var v when v == map.GetExp("shok"):
                    rightBrow.transform.localPosition = new Vector3(-175, 63, 0);
                    rightBrow.transform.localRotation = Quaternion.Euler(0, 0, 75);
                    leftBrow.transform.localPosition = new Vector3(-10, 50, 0);
                    leftBrow.transform.localRotation = Quaternion.Euler(0, 0, -50);
                    break;

                case var v when v == map.GetExp("grin"):
                    rightBrow.transform.localPosition = new Vector3(-175, 47, 0);
                    rightBrow.transform.localRotation = Quaternion.Euler(0, 0, 55);
                    leftBrow.transform.localPosition = new Vector3(-30, 40, 0);
                    leftBrow.transform.localRotation = Quaternion.Euler(0, 0, -45);
                    break;

                default:
                    // both eyes norm outline
                    expressions[0].gameObject.SetActive(true);
                    expressions[1].gameObject.SetActive(true);

                    leftBrow.transform.localPosition = new Vector3(-45, 38, 0);
                    leftBrow.transform.localRotation = Quaternion.Euler(0, 0, -10);
                    rightBrow.transform.localPosition = new Vector3(-165, 50, 0);
                    rightBrow.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    break;
            }
        }
    }
}
