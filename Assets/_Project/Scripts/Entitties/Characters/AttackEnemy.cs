using Cysharp.Threading.Tasks;
using System.Threading;
using GameScene.Level;
using System;
using UnityEngine;

namespace GameScene.Characters
{
    public abstract class AttackEnemy : MonoBehaviour
    {
        public event Action DestroyCharacter;

        public event Action<string> CreateText;

        public event Action<int, int> ChangeHPBar;

        protected CancellationTokenSource CtsAttack;

        protected bool PerkIsActive = false;

        private EndPanelSettings _endPanelSettings;

        private CalculateDamage _calculateDamage = new CalculateDamage();

        [field: SerializeField]
        public CharacterData CharacterData { get; private set; }

        public Character Character { get; private set; }

        public async void StartTaskAttack(AttackEnemy enemy, AttackEnemy character)
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
        {
            do
            {
                if (CtsAttack == null || CtsAttack.IsCancellationRequested)
                {
                    break;
                }

                int damage = _calculateDamage.CalculatingDamage(character, Character.OneWayDamageSpread);

                AddValueToHealthEnemy(damage, enemy);

                enemy.CreateText.Invoke($"{damage} HP");

                CheckHPEnemy(enemy);

                await UniTask.Delay(TimeSpan.FromSeconds(Character.Cooldown));

                if (UnityEngine.Random.value < (float)Character.PercentagesChancePerk / 100
                    && !PerkIsActive
                    && enemy != null)
                {
                    UsePerk(enemy, character);
                }

                await UniTask.Delay(TimeSpan.FromSeconds(0.5f));

            } while (enemy != null);
        }

        private void CheckHPEnemy(AttackEnemy enemy)
        {
            if (Character.HealthEntity == 0)
            {
                enemy.DestroyCharacter.Invoke();
                _endPanelSettings.ActivateEndPanel(Character.TextNameEntity);

                StopTaskAttack();
            }
        }

        private async void UsePerk(AttackEnemy enemy, AttackEnemy character)
        {
            character.CreateText.Invoke($"Персонаж использовал перк: {Character.TextApplicationsPerk}");

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