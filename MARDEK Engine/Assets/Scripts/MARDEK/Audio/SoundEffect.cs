using UnityEngine;

namespace MARDEK.Audio
{
    [CreateAssetMenu(menuName = "MARDEK/Audio/SoundEffect")]
    public class SoundEffect : AudioObject
    {
        public override void PlayOnSource(AudioSource audioSource)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}