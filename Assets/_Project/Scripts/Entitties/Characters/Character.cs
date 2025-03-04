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

        public IntValue Damage { get; private set; }
        public IntValue CoefChangeDamage { get; private set; }
        public IntValue Cooldown { get; private set; }
        public IntValue OneWayDamageSpread { get; private set; }
        public IntValue HealthEntity { get; private set; }
        public IntValue MaxHealthEntity { get; private set; }
        public IntValue DurationPerk { get; private set; }
        public IntValue ChancePerk { get; private set; }

        public string NamePerk { get; private set; }

        public string NameEntity { get; private set; }

        protected CancellationTokenSource TokenSourceAttack;

        protected CancellationTokenSource TokenSourcePerk;

        protected bool IsPerkActive = false;

        private EndPanel _endPanel;

        private CharacterScriptableData _entityData;

        private CalculateDamage _calculateDamage = new CalculateDamage();

        [field: SerializeField]
        public CharacterScriptableData CharacterData { get; private set; }

        public Character(CharacterScriptableData entityData, string nameEntity, EndPanel endPanelSettings)
        {
            _entityData = entityData;
            InitializeElements(nameEntity);

            _endPanel = endPanelSettings;
        }

        private void InitializeElements(string nameEntity)
        {
            CoefChangeDamage = new IntValue(0);
            HealthEntity = new IntValue(_entityData.MaxHealthEntity);
            MaxHealthEntity = new IntValue(_entityData.MaxHealthEntity);
            Cooldown = new IntValue(_entityData.Cooldown);
            OneWayDamageSpread = new IntValue(_entityData.OneWayDamageSpread);
            Damage = new IntValue(_entityData.Damage);
            DurationPerk = new IntValue(_entityData.DurationPerk);
            ChancePerk = new IntValue(Mathf.Clamp(_entityData.PercentagesChancePerk, 0, 100));
            NamePerk = _entityData.TextApplicationsPerk;
            NameEntity = nameEntity;
        }

        public void ChangeCoefDamage(int newValue)
        {
            newValue = Math.Max(0, newValue);
            CoefChangeDamage = new IntValue(Math.Max(0, newValue));
        }

        public async void StartAttack(Character enemy)
        {
            if (TokenSourceAttack == null || TokenSourceAttack.IsCancellationRequested)
            {
                TokenSourceAttack = new CancellationTokenSource();
            }

            await Attack(enemy);
        }

        public void StopAttack()
        {
            TokenSourceAttack?.Cancel();
        }

        public void TakeDamage(int value)
        {
            ChangeHealth(value);

            OnCreateText.Invoke($"{value} HP");

            if (HealthEntity == 0)
            {
                StartDestroy();
            }
        }

        public void StartDestroy()
        {
            StopAttack();
            StopPerk();

            _endPanel.Activate(NameEntity);

            CharacterDestroyed.Invoke();
        }

        protected abstract UniTask Perk(Character enemy);

        protected void StopPerk()
        {
            TokenSourcePerk?.Cancel();
        }

        private async void TryUsePerk(Character enemy)
        {
            float randomValue = UnityEngine.Random.value;

            if (randomValue < (float)ChancePerk / 100
                && !IsPerkActive
                && enemy != null)
            {
                if (TokenSourcePerk == null || TokenSourcePerk.IsCancellationRequested)
                {
                    TokenSourcePerk = new CancellationTokenSource();
                }

                OnCreateText.Invoke($"Персонаж использовал перк: {NamePerk}");

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

        private void ChangeHealth(int valueChange)
        {
            int newHealth = Mathf.Clamp(HealthEntity + valueChange, 0, MaxHealthEntity);
            HealthEntity.Set(newHealth);
        }

        private async UniTask Attack(Character enemy)
        {
            do
            {
                if (enemy.HealthEntity == 0 || TokenSourceAttack == null || TokenSourceAttack.IsCancellationRequested)
                    break;

                int damage = _calculateDamage.Calculating(this, OneWayDamageSpread);

                enemy.TakeDamage(damage);

                await UniTask.Delay(TimeSpan.FromSeconds(Cooldown), cancellationToken: TokenSourceAttack.Token);

                TryUsePerk(enemy);

                await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: TokenSourceAttack.Token);

            } while (TokenSourceAttack != null && !TokenSourceAttack.IsCancellationRequested);
        }
    }
}