using UnityEngine;
using MARDEK.Event;

namespace MARDEK.Save
{
    [System.Serializable]
    public class LocalSwitchBool : AddressableMonoBehaviour, IBoolCheck
    {
        [SerializeField] protected bool value = false;

        public bool GetBoolValue()
        {
            return value;
        }

        public virtual void SetBoolValue(bool setValue)
        {
            value = setValue;
        }
    }
}