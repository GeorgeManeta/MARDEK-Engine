using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MARDEK.CharacterSystem;
using MARDEK.Stats;

namespace MARDEK.Battle
{
    public class BattleManager : MonoBehaviour
    {
        public static EncounterSet encounter { private get; set; }
        [SerializeField] FloatStat ACTStat = null;
        [SerializeField] IntegerStat AGLStat = null;
        [SerializeField] Party playerParty;
        [SerializeField] List<Character> enemies = new List<Character>();

        public List<Character> playableCharacters
        {
            get
            {
                return playerParty.Characters;
            }
        }
        public List<Character> enemyCharacters
        {
            get
            {
                return enemies;
                //var chars = new List<Character>();
                //foreach (var enemy in enemies)
                //    chars.Add(enemy.GetComponent<Character>());
                //return chars;
            }
        }

        public static Character characterActing { get; private set; }
        [SerializeField] GameObject characterActionUI = null;

        public static Stats.IActionSlot selectedAction { get; set; }
        public static List<Character> targets { get; private set; }
        const float actResolution = 2;

        private void Awake()
        {
            if(encounter) enemies = encounter.InstantiateEncounter();
        }
        private void Update()
        {
            if (characterActing == null)
            {
                characterActing = StepActCycleTryGetNextCharacter();
                if (characterActing != null)
                {
                    characterActionUI.SetActive(true);
                }
            }
            else
            {
                if (selectedAction != null)
                {
                    Character target;
                    if (enemyCharacters.Contains(characterActing))
                    {
                        target = playableCharacters[Random.Range(0, playableCharacters.Count-1)];
                    }
                    else
                    {
                        target = enemyCharacters[Random.Range(0, enemyCharacters.Count-1)];
                    }

                    selectedAction.ApplyAction(characterActing, target);
                    Debug.Log($"{characterActing.CharacterInfo.displayName} uses {selectedAction.DisplayName} targeting {target}");
                    selectedAction = null;
                    characterActing = null;
                    characterActionUI.SetActive(false);
                }
            }
        }
        Character StepActCycleTryGetNextCharacter()
        {
            var charactersInBattle = GetCharactersInOrder();
            AddTickRateToACT(ref charactersInBattle, Time.deltaTime);
            var readyToAct = GetNextCharacterReadyToAct(charactersInBattle);
            if (readyToAct != null)
                readyToAct.ModifyStat(ACTStat, -actResolution); // "reset" charact ACT
            return readyToAct;
        }
        List<Character> GetCharactersInOrder()
        {
            // order by position (p1 e1 p2 e2 p3 e3 p4 e4)
            List<Character> returnList = new List<Character>();
            for (int i = 0; i < 4; i++)
            {
                if (playableCharacters.Count > i)
                    returnList.Add(playableCharacters[i]);
                if (enemyCharacters.Count > i)
                    returnList.Add(enemyCharacters[i]);
            }
            return returnList;
        }
        void AddTickRateToACT(ref List<Character> characters, float deltatime)
        {
            foreach(var c in characters)
            {
                var tickRate = 1 + 0.05f * c.GetStat(AGLStat).Value;
                tickRate *= deltatime;
                c.ModifyStat(ACTStat, tickRate);
            }
        }
        Character GetNextCharacterReadyToAct(List<Character> characters)
        {
            float maxAct = 0;
            foreach(var c in characters)
            {
                var act = c.GetStat(ACTStat).Value;
                if (act > maxAct)
                    maxAct = act;
            }
            if (maxAct < actResolution)
                return null;
            foreach (var c in characters)
                if (c.GetStat(ACTStat).Value == maxAct)
                    return c;
            throw new System.Exception("A character had enough ACT to take a turn but wasn't returned by this method");
        }
        public void SkipCurrentCharacterTurn()
        {
            characterActing = null;
            characterActionUI.SetActive(false);
        }
    }
}