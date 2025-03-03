using Cysharp.Threading.Tasks;
using GameScene.Level;
using System;
using System.Threading;
using UnityEngine;

namespace GameScene.Characters
{
    public abstract class Character
    {
        public event Action CharacterDestroyed;

        public event Action<string> OnCreateText;

        public event Action<int, int> HealthChanged;

        public IntValue BaseDamage { get; private set; }
        public IntValue CoefChangeDamage { get; private set; }
        public IntValue Cooldown { get; private set; }
        public IntValue OneWayDamageSpread { get; private set; }
        public IntValue HealthEntity { get; private set; }
        public IntValue MaxHealthEntity { get; private set; }
        public IntValue DurationPerk { get; private set; }
        public IntValue PercentagesChancePerk { get; private set; }

        public string TextApplicationsPerk { get; private set; }

        public string TextNameEntity { get; private set; }

        protected CancellationTokenSource CtsAttack;

        protected bool IsPerkActive = false;

        private EndPanel _endPanelSettings;

        private CharacterScriptableData _entityData;

        private CalculateDamage _calculateDamage = new CalculateDamage();

        public Character(CharacterScriptableData entityData, string nameEntity, EndPanel endPanelSettings)
        {
            _entityData = entityData;
            InitializeElements(nameEntity);

            _endPanelSettings = endPanelSettings;
        }

        private void InitializeElements(string nameEntity)
        {
            CoefChangeDamage = 0;
            HealthEntity = _entityData.MaxHealthEntity;
            MaxHealthEntity = _entityData.MaxHealthEntity;
            Cooldown = _entityData.Cooldown;
            OneWayDamageSpread = _entityData.OneWayDamageSpread;
            BaseDamage = _entityData.Damage;
            DurationPerk = _entityData.DurationPerk;
            PercentagesChancePerk = Mathf.Clamp(_entityData.PercentagesChancePerk, 0, 100);
            TextApplicationsPerk = _entityData.TextApplicationsPerk;
            TextNameEntity = nameEntity;
        }

        public void ChangeCoefChangeDamage(int newValue)
        {
            if (newValue > 0)
                CoefChangeDamage = new IntValue(newValue);
            else
                CoefChangeDamage = new IntValue(0);
        }

        public async void StartAttack(Character enemy)
        {
            if (CtsAttack == null || CtsAttack.IsCancellationRequested)
            {
                CtsAttack = new CancellationTokenSource();
            }

            await Attack(enemy);
        }

        public void StopAttack()
        {
            CtsAttack?.Cancel();
        }

        public void TakeDamage(int value)
        {
            AddHealthValue(value);

            if (HealthChanged != null)
                HealthChanged.Invoke(HealthEntity, MaxHealthEntity);

            if (HealthEntity == 0)
            {
                StartDestroy();
            }
        }

        public void StartDestroy()
        {
            StopAttack();
            _endPanelSettings.Activate(TextNameEntity);

            CharacterDestroyed.Invoke();
        }

        protected abstract UniTask Perk(Character enemy);

        private async void TryUsePerk(Character enemy)
        {
            float randomValue = UnityEngine.Random.value;

            if (randomValue < (float)PercentagesChancePerk / 100
                && !IsPerkActive
                && enemy != null)
            {
                OnCreateText.Invoke($"Персонаж использовал перк: {TextApplicationsPerk}");

                try
                {
                    IsPerkActive = true;
                    await Perk(enemy);
                }
                finally
                {
                    IsPerkActive = false;
                }
            }
        }

        private void AddHealthValue(int valueChange)
        {
            HealthEntity = Mathf.Clamp(HealthEntity + valueChange, 0, MaxHealthEntity);
        }

        private async UniTask Attack(Character enemy)
        {
            do
            {
                int damage = _calculateDamage.CalculatingDamage(this, OneWayDamageSpread);

                enemy.AddHealthValue(damage);

                OnCreateText.Invoke($"{damage} HP");

                await UniTask.Delay(TimeSpan.FromSeconds(Cooldown));

                TryUsePerk(enemy);

                await UniTask.Delay(TimeSpan.FromSeconds(0.5f));

            } while (CtsAttack != null && !CtsAttack.IsCancellationRequested);
        }
    }
}