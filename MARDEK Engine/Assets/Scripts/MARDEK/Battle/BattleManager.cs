using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MARDEK.CharacterSystem;
using MARDEK.Stats;

namespace MARDEK.Battle
{
    using Core;
    using Progress;
    using UnityEngine.Events;

    public class BattleManager : MonoBehaviour
    {
        [SerializeField] IntegerStat ACTStat = null;
        [SerializeField] IntegerStat AGLStat = null;
        [SerializeField] Party playerParty;
        [SerializeField] List<Character> DummyEnemies;
        [SerializeField] GameObject characterActionUI = null;
        [SerializeField] List<GameObject> enemyPartyPositions = new();
        [SerializeField] List<GameObject> playerPartyPositions = new();
        [SerializeField] UnityEvent OnVictory;
        public static EncounterSet encounter { private get; set; }
        public static BattleCharacter characterActing { get; private set; }
        public static IActionSlot selectedAction { get; set; }
        const float actResolution = 1000;
        public List<BattleCharacter> EnemyBattleParty { get; private set; } = new();
        public List<BattleCharacter> PlayerBattleParty { get; private set; } = new();

        private void Awake()
        {
            List<Character> enemyCharacters;
            if (encounter)
                enemyCharacters = encounter.InstantiateEncounter();
            else
                enemyCharacters = DummyEnemies;
            foreach (var c in enemyCharacters)
                SpawnEnemyBattleCharacter(c);
            foreach (var c in playerParty.Characters)
                SpawnPlayerBattleCharacter(c);
        }

        void SpawnEnemyBattleCharacter(Character c)
        {
            var position = enemyPartyPositions[EnemyBattleParty.Count].transform.position;
            EnemyBattleParty.Add(new BattleCharacter(c, position));
        }
        void SpawnPlayerBattleCharacter(Character c)
        {
            var position = playerPartyPositions[PlayerBattleParty.Count].transform.position;
            PlayerBattleParty.Add(new BattleCharacter(c, position));
        }

        private void Update()
        {
            CheckVictory();
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
                    // pick random target for now
                    BattleCharacter target;
                    if (EnemyBattleParty.Contains(characterActing))
                        target = PlayerBattleParty[Random.Range(0, PlayerBattleParty.Count-1)];
                    else
                        target = EnemyBattleParty[Random.Range(0, EnemyBattleParty.Count-1)];

                    Debug.Log($"{characterActing.Name} targets {target.Name}");
                    selectedAction.ApplyAction(characterActing, target);
                    selectedAction = null;
                    characterActing = null;
                    characterActionUI.SetActive(false);
                }
            }
        }
        BattleCharacter StepActCycleTryGetNextCharacter()
        {
            var charactersInBattle = GetCharactersInOrder();
            AddTickRateToACT(ref charactersInBattle, Time.deltaTime);
            var readyToAct = GetNextCharacterReadyToAct(charactersInBattle);
            if (readyToAct != null)
                readyToAct.ModifyStat(ACTStat, -(int)actResolution); // "reset" charact ACT
            return readyToAct;
        }
        List<BattleCharacter> GetCharactersInOrder()
        {
            // order by Position (p1 e1 p2 e2 p3 e3 p4 e4)
            List<BattleCharacter> returnList = new List<BattleCharacter>();
            for (int i = 0; i < 4; i++)
            {
                if (PlayerBattleParty.Count > i)
                    returnList.Add(PlayerBattleParty[i]);
                if (EnemyBattleParty.Count > i)
                    returnList.Add(EnemyBattleParty[i]);
            }
            return returnList;
        }
        
        void AddTickRateToACT(ref List<BattleCharacter> characters, float deltatime)
        {
            foreach(var c in characters)
            {
                var tickRate = 1 + 0.05f * c.GetStat(AGLStat);
                tickRate *= 1000 * deltatime;
                c.ModifyStat(ACTStat, (int)tickRate);
            }
        }
        BattleCharacter GetNextCharacterReadyToAct(List<BattleCharacter> characters)
        {
            float maxAct = 0;
            foreach(var c in characters)
            {
                var act = c.GetStat(ACTStat);
                if (act > maxAct)
                    maxAct = act;
            }
            if (maxAct < actResolution)
                return null;
            foreach (var c in characters)
                if (c.GetStat(ACTStat) == maxAct)
                    return c;
            throw new System.Exception("A character had enough ACT to take a turn but wasn't returned by this method");
        }
        public void SkipCurrentCharacterTurn()
        {
            characterActing = null;
            characterActionUI.SetActive(false);
        }
    
        void CheckVictory()
        {
            for (int i = EnemyBattleParty.Count - 1; i >= 0; i--)
            {
                var enemy = EnemyBattleParty[i];
                var health = enemy.GetStat(StatsGlobals.Instance.CurrentHP);
                if (health <= 0)
                    EnemyBattleParty.Remove(enemy);
            }

            var victory = EnemyBattleParty.Count == 0;
            if (victory)
            {
                print("victory!!");
                OnVictory.Invoke();
                enabled = false;
            }
        }
    }
}