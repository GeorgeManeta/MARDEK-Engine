using UnityEngine;

namespace MARDEK.CharacterSystem
{
    public class RobotGearRotate : MonoBehaviour
    {
        [SerializeField] bool rotateCounterClockwise = false;

        private void OnEnable()
        {
            Animator animator = GetComponent<Animator>();
            if (rotateCounterClockwise)
            {
                animator.SetFloat("SpeedMultiplier", -1);
            }
            else
            {
                animator.SetFloat("SpeedMultiplier", 1);
            }
        }
    }
}
