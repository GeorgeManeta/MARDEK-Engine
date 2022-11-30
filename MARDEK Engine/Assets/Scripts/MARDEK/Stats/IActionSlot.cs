using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MARDEK.Stats
{
    public interface IActionSlot
    {
        public string DisplayName { get; }
        public Sprite Sprite { get; }
        public int Number { get; }
        public string Description { get; }
        public void ApplyAction(IStats user, IStats target);
    }
}