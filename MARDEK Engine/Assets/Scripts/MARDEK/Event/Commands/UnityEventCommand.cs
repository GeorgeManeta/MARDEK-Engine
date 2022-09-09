using UnityEngine;
using UnityEngine.Events;

namespace MARDEK.Event
{
    public class UnityEventCommand : CommandBase
    {
        [SerializeField] UnityEvent _event = default;

        public override void Trigger()
        {
            _event.Invoke();
        }
    }
}