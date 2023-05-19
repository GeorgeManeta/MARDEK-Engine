using UnityEngine;

namespace MARDEK.CharacterSystem
{
    public class HumanBlinkRandom : StateMachineBehaviour
    {
        int noBlinkHash = Animator.StringToHash("Base Layer" + ".NoMotion");

        float timeWithoutBlinking = 0;
        float timeBetweenBlinks = 1;

        // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            timeWithoutBlinking = 0;
            timeBetweenBlinks = (float)(Random.value * 4 + 0.75);
        }

        // OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateinfo, int layerindex)
        {
            if (stateinfo.fullPathHash == noBlinkHash && !animator.GetBool("TriggerBlink"))
            {
                timeWithoutBlinking += Time.deltaTime;

                if (timeWithoutBlinking >= timeBetweenBlinks)
                {
                    animator.SetTrigger("TriggerBlink");
                }
            }
        }
    }
}
