using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using GameScene.Repositories;
using UnityEngine;

namespace GameScene.Characters
{
    public abstract class Character
    {
        public event Action OnCharacterDestroy;
        
        public event Action<string> OnWin;

        public event Action<string> OnHarmEnemy;

        public IntValue Damage { get; private set; }
        
        public IntValue HealthCharacter { get; private set; }
        
        public int MaxHealthEntity { get; private set; }
        
        protected bool IsPerkActive;
        
        protected readonly int DurationPerk;
        
        protected readonly int ForcePerk;
        
        protected CancellationTokenSource TokenSourcePerk;
        
        private CancellationTokenSource _tokenSourceAttack;
        
        private readonly float _delayAfterPerk;
        
        private readonly int _cooldown;
        
        private readonly int _oneWayDamageSpread;
        
        private readonly int _chancePerk;
        
        private readonly string _namePerk;
        
        private readonly string _nameEntity;
        
        private readonly CalculatorDamage _calculatorDamage = new CalculatorDamage();

        protected Character(CharacterConfig entityConfig, NamesRepository namesRepository)
        {
            HealthCharacter = new IntValue(entityConfig.MaxHealthEntity);
            MaxHealthEntity = new IntValue(entityConfig.MaxHealthEntity);
            _cooldown = new IntValue(entityConfig.Cooldown);
            _oneWayDamageSpread = new IntValue(entityConfig.OneWayDamageSpread);
            _delayAfterPerk = entityConfig.DelayAfterPerk;
            Damage = new IntValue(entityConfig.Damage);
            ForcePerk = entityConfig.ForcePerk;
            DurationPerk = new IntValue(entityConfig.DurationPerk);
            _chancePerk = new IntValue(Mathf.Clamp(entityConfig.PercentagesChancePerk, 0, 100));
            _namePerk = entityConfig.TextApplicationsPerk;
            _nameEntity = namesRepository.GetRandomName();
        }

        public async void StartAttack(Character enemy)
        {
            if (_tokenSourceAttack == null || _tokenSourceAttack.IsCancellationRequested)
            {
                _tokenSourceAttack = new CancellationTokenSource();
            }

            await Attack(enemy);
        }

        public void GetDamage(int value, Character enemy)
        {
            int newHealth = Mathf.Clamp(HealthCharacter - Math.Abs(value), 0, MaxHealthEntity);
            if (value < 0)
            {
                Debug.LogError("Значение урона отрицательное!");
            }
            
            HealthCharacter.Set(newHealth);

            OnHarmEnemy?.Invoke($"-{value} HP");

            if (HealthCharacter == 0)
            {
                enemy.Win();
                StartDestroy();
            }
        }

        public void StartDestroy()
        {
            StopAttack();
            StopPerk();
            
            OnCharacterDestroy?.Invoke();
        }
        
        public void ChangeDamage(int newValue)
        {
            newValue = Math.Max(0, newValue);
            Damage.Set(newValue);
        }

        protected abstract UniTask Perk(Character enemy);
        
        private void Win()
        {
            OnWin?.Invoke(_nameEntity);
        }
        
        private void StopAttack()
        {
            _tokenSourceAttack?.Cancel();
        }

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

                OnHarmEnemy?.Invoke($"Персонаж использовал перк: {_namePerk}");

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

        private async UniTask Attack(Character enemy)
        {
            do
            {
                if (enemy.HealthCharacter == 0 || _tokenSourceAttack == null || _tokenSourceAttack.IsCancellationRequested)
                    break;

                int damage = _calculatorDamage.Calculate(this, _oneWayDamageSpread);

                enemy.GetDamage(damage, enemy);

                await UniTask.Delay(TimeSpan.FromSeconds(_cooldown), cancellationToken: _tokenSourceAttack.Token);

                TryUsePerk(enemy);

                await UniTask.Delay(TimeSpan.FromSeconds(_delayAfterPerk), cancellationToken: _tokenSourceAttack.Token);

            } while (_tokenSourceAttack?.IsCancellationRequested == false);
        }
    }
}