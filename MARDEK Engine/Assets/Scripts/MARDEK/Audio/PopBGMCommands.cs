using MARDEK.Event;

namespace MARDEK.Audio
{
    public class PopBGMCommands : CommandBase
    {
        public override void Trigger()
        {
            AudioManager.PopMusic();
        }
    }
}