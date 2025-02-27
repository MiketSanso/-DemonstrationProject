using Cysharp.Threading.Tasks;
using System.Threading;
using GameScene.Level;
<<<<<<< HEAD
using System;
using UnityEngine;

namespace GameScene.Characters
{
    public abstract class AttackEnemy : MonoBehaviour
    {
        public event Action DestroyCharacter;

        public event Action<string> CreateText;

        public event Action<int, int> ChangeHPBar;

=======
using UnityEngine;

namespace GameScene.Character
{
    public abstract class AttackEnemy : MonoBehaviour
    {
>>>>>>> 1ae4312d68fd11c59cbe48c26c578b07b4bdda25
        protected CancellationTokenSource CtsAttack;

        protected bool PerkIsActive = false;

        private EndPanelSettings _endPanelSettings;

<<<<<<< HEAD
        private CalculateDamage _calculateDamage = new CalculateDamage();

        [field: SerializeField]
        public CharacterData CharacterData { get; private set; }

        public Character Character { get; private set; }

        public async void StartTaskAttack(AttackEnemy enemy, AttackEnemy character)
=======
        private CalculateDamage calculateDamage = new CalculateDamage();

        public async void StartTaskAttack(CharacterUI enemy, CharacterUI character)
>>>>>>> 1ae4312d68fd11c59cbe48c26c578b07b4bdda25
        {
            if (CtsAttack == null || CtsAttack.IsCancellationRequested)
            {
                CtsAttack = new CancellationTokenSource();
            }

            await TakeDamage(enemy, character);
        }

        public void StopTaskAttack()
        {
            CtsAttack?.Cancel();
        }

<<<<<<< HEAD
        public void InitializeVariables(EndPanelSettings endPanelSettings, Character character)
        {
            _endPanelSettings = endPanelSettings;
            Character = character;
        }

        protected abstract UniTask Perk(AttackEnemy enemy, AttackEnemy character);

        protected void AddValueToHealthEnemy(int value, AttackEnemy enemy)
        {
            Character.AddingValueToHealth(value);

            if (enemy.ChangeHPBar != null)
                enemy.ChangeHPBar.Invoke(Character.HealthEntity, Character.MaxHealthEntity);
        }

        private async UniTask TakeDamage(AttackEnemy enemy, AttackEnemy character)
=======
        public void InitializeVariables(EndPanelSettings endPanelSettings)
        {
            _endPanelSettings = endPanelSettings;
        }

        protected abstract UniTask Perk(CharacterUI enemy, CharacterUI character);

        protected void AddValueToHealthEnemy(int value, CharacterUI enemy)
        {
            enemy.Character.AddingValueToHealth(value);
            enemy.HpBar.ChangeHPBar(enemy.Character.HealthEntity.Get(), enemy.Character.MaxHealthEntity.Get());
        }

        private async UniTask TakeDamage(CharacterUI enemy, CharacterUI character)
>>>>>>> 1ae4312d68fd11c59cbe48c26c578b07b4bdda25
        {
            do
            {
                if (CtsAttack == null || CtsAttack.IsCancellationRequested)
                {
                    break;
                }

<<<<<<< HEAD
                int damage = _calculateDamage.CalculatingDamage(character, Character.OneWayDamageSpread);

                AddValueToHealthEnemy(damage, enemy);

                enemy.CreateText.Invoke($"{damage} HP");

                CheckHPEnemy(enemy);

                await UniTask.Delay(TimeSpan.FromSeconds(Character.Cooldown));

                if (UnityEngine.Random.value < (float)Character.PercentagesChancePerk / 100
=======
                int damage = calculateDamage.CalculatingDamage(character, character.Character.OneWayDamageSpread.Get());

                AddValueToHealthEnemy(damage, enemy);

                await enemy.CreateText($"{damage} HP");

                CheckHPEnemy(enemy);

                await UniTask.Delay(character.Character.Cooldown.Get() * 1000);

                if (Random.Range(0, 101) < character.Character.PercentagesChancePerk.Get()
>>>>>>> 1ae4312d68fd11c59cbe48c26c578b07b4bdda25
                    && !PerkIsActive
                    && enemy != null)
                {
                    UsePerk(enemy, character);
                }

<<<<<<< HEAD
                await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
=======
                await UniTask.Delay(500);
>>>>>>> 1ae4312d68fd11c59cbe48c26c578b07b4bdda25

            } while (enemy != null);
        }

<<<<<<< HEAD
        private void CheckHPEnemy(AttackEnemy enemy)
        {
            if (Character.HealthEntity == 0)
            {
                enemy.DestroyCharacter.Invoke();
                _endPanelSettings.ActivateEndPanel(Character.TextNameEntity);
=======
        private void CheckHPEnemy(CharacterUI enemy)
        {
            if (enemy.Character.HealthEntity.Get() == 0)
            {
                enemy.DestroyThisObject();
                _endPanelSettings.ActivateEndPanel(enemy.Character.TextNameEntity);
>>>>>>> 1ae4312d68fd11c59cbe48c26c578b07b4bdda25

                StopTaskAttack();
            }
        }

<<<<<<< HEAD
        private async void UsePerk(AttackEnemy enemy, AttackEnemy character)
        {
            character.CreateText.Invoke($"Персонаж использовал перк: {Character.TextApplicationsPerk}");
=======
        private async void UsePerk(CharacterUI enemy, CharacterUI character)
        {
            await character.CreateText($"Персонаж использовал перк: {character.Character.TextApplicationsPerk}");
>>>>>>> 1ae4312d68fd11c59cbe48c26c578b07b4bdda25

            try
            {
                PerkIsActive = true;
                await Perk(enemy, character);
            }
            finally
            {
                PerkIsActive = false;
            }
        }
    }
}