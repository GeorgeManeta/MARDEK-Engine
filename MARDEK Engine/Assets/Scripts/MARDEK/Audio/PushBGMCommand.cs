using UnityEngine;
using MARDEK.Event;

namespace MARDEK.Audio
{
    public class PushBGMCommand : CommandBase
    {
        [SerializeField] Music music;

        public override void Trigger()
        {
            AudioManager.PushMusic(music);
        }
    }
}