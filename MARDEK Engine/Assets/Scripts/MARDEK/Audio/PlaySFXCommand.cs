using UnityEngine;
using MARDEK.Event;

namespace MARDEK.Audio
{
    public class PlaySFXCommand : CommandBase
    {
        [SerializeField] SoundEffect sound;

        public override void Trigger()
        {
            AudioManager.PlaySoundEffect(sound);
        }
    }
}