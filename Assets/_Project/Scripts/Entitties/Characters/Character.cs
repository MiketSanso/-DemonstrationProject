using Cysharp.Threading.Tasks;
using GameScene.Level;
using System;
using System.Threading;
using UnityEngine;

namespace GameScene.Characters
{
    public abstract class Character
    {
        public event Action OnCharacterDestroy;

        public event Action<string> OnCreateText;

        public IntValue Damage { get; private set; }
        
        public IntValue CoefChangeDamage { get; private set; }
        
        public IntValue HealthEntity { get; private set; }
        
        public int MaxHealthEntity { get; private set; }
        
        protected bool IsPerkActive;
        
        protected readonly int DurationPerk;
        
        protected CancellationTokenSource TokenSourcePerk;
        
        private CancellationTokenSource _tokenSourceAttack;
        
        private readonly int _cooldown;
        
        private readonly int _oneWayDamageSpread;
        
        private readonly int _chancePerk;
        
        private readonly CalculatorDamage _calculatorDamage = new CalculatorDamage();
        
        private readonly string _namePerk;

        public string NameEntity { get; private set; }

        protected Character(CharacterConfig entityConfig, string nameEntity)
        {
            CoefChangeDamage = new IntValue(0);
            HealthEntity = new IntValue(entityConfig.MaxHealthEntity);
            MaxHealthEntity = new IntValue(entityConfig.MaxHealthEntity);
            _cooldown = new IntValue(entityConfig.Cooldown);
            _oneWayDamageSpread = new IntValue(entityConfig.OneWayDamageSpread);
            Damage = new IntValue(entityConfig.Damage);
            DurationPerk = new IntValue(entityConfig.DurationPerk);
            _chancePerk = new IntValue(Mathf.Clamp(entityConfig.PercentagesChancePerk, 0, 100));
            _namePerk = entityConfig.TextApplicationsPerk;
            NameEntity = nameEntity;
        }

        public void ChangeCoefDamage(int newValue)
        {
            newValue = Math.Max(0, newValue);
            CoefChangeDamage = new IntValue(Math.Max(0, newValue));
        }

        public async void StartAttack(Character enemy)
        {
            if (_tokenSourceAttack == null || _tokenSourceAttack.IsCancellationRequested)
            {
                _tokenSourceAttack = new CancellationTokenSource();
            }

            await Attack(enemy);
        }

        public void StopAttack()
        {
            _tokenSourceAttack?.Cancel();
        }

        public void TakeDamage(int value)
        {
            ChangeHealth(value);

            OnCreateText?.Invoke($"{value} HP");

            if (HealthEntity == 0)
            {
                StartDestroy();
            }
        }

        public void StartDestroy()
        {
            StopAttack();
            StopPerk();
            
            OnCharacterDestroy?.Invoke();
        }

        protected abstract UniTask Perk(Character enemy);

        private void StopPerk()
        {
            TokenSourcePerk?.Cancel();
        }

        private async void TryUsePerk(Character enemy)
        {
            float randomValue = UnityEngine.Random.value;

            if (randomValue < (float)_chancePerk / 100
                && !IsPerkActive
                && enemy != null)
            {
                if (TokenSourcePerk == null || TokenSourcePerk.IsCancellationRequested)
                {
                    TokenSourcePerk = new CancellationTokenSource();
                }

                OnCreateText?.Invoke($"�������� ����������� ����: {_namePerk}");

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
                if (enemy.HealthEntity == 0 || _tokenSourceAttack == null || _tokenSourceAttack.IsCancellationRequested)
                    break;

                int damage = _calculatorDamage.Calculate(this, _oneWayDamageSpread);

                enemy.TakeDamage(damage);

                await UniTask.Delay(TimeSpan.FromSeconds(_cooldown), cancellationToken: _tokenSourceAttack.Token);

                TryUsePerk(enemy);

                await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: _tokenSourceAttack.Token);

            } while (_tokenSourceAttack?.IsCancellationRequested == false);
        }
    }
}