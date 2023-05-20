using System.Collections.Generic;
using UnityEngine;
using CharacterProfile = MARDEK.CharacterSystem.CharacterProfile;
using PortraitExpressionEnum = MARDEK.CharacterSystem.PortraitExpressionEnum;

namespace MARDEK.DialogueSystem
{
    [System.Serializable]
    public class CharacterLines
    {
        [field: SerializeField] public PortraitExpressionEnum Expression { get; private set; }
        [field: SerializeField] public CharacterProfile Character { get; private set; }
        [field: TextArea(0,5)]
        [field: SerializeField] public List<string> Lines { get; private set; }

        public CharacterLines(CharacterProfile info, List<string> lines, PortraitExpressionEnum expression)
        {
            Character = info;
            Lines = lines;
            Expression = expression;
        }
    }
}