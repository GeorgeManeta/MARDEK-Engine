using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MARDEK.Animation
{
    using Event;
    public class TransitionEffectCommand : OngoingCommand
    {
        [SerializeField] TransitionEffectManager.TransitionEffectType type;
        public override bool IsOngoing()
        {
            return TransitionEffectManager.Wait();
        }

        public override void Trigger()
        {
            TransitionEffectManager.PlayEffect(type);
        }
    }
}