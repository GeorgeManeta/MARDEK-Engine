using System.Collections.Generic;
using UnityEngine;
using CharacterProfile = MARDEK.CharacterSystem.CharacterProfile;
using PortraitExpressionEnum = MARDEK.CharacterSystem.PortraitExpressionEnum;

namespace MARDEK.DialogueSystem
{
    [System.Serializable]
    public class CharacterLines
    {
        [field: SerializeField] public CharacterProfile Character { get; private set; }
        [field: SerializeField] public List<LineWrapper> WrappedLines { get; set; }

        [System.Serializable]
        public struct LineWrapper
        {
            public PortraitExpressionEnum expression;
            [TextArea(0, 5)] 
            public string line;
        }
    }
}