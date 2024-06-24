using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MARDEK.Core
{
    public interface IActionSlot
    {
        public string DisplayName { get; }
        public Sprite Sprite { get; }
        public int Number { get; }
        public string Description { get; }
        public void ApplyAction(IActor user, IActor target);
    }
}