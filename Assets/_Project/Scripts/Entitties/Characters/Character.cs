using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using GameScene.Level.Texts;
using GameScene.Repositories;
using UnityEngine;

namespace GameScene.Characters
{
    public abstract class Character
    {
        public event Action OnCharacterDestroy;
        public event Action<Character> OnWin;
        public event Action<string, TypesText> OnHarmEnemy;
        public IntValue Damage { get; private set; }
        public IntValue Health { get; private set; }
        public int MaxHealth { get; private set; }

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
        private readonly CalculatorDamage _calculatorDamage = new CalculatorDamage();

        public readonly string NameCharacter;

        protected Character(CharacterConfig entityConfig, NamesRepository namesRepository)
        {
            Health = new IntValue(entityConfig.MaxHealthEntity);
            MaxHealth = new IntValue(entityConfig.MaxHealthEntity);
            _cooldown = new IntValue(entityConfig.Cooldown);
            _oneWayDamageSpread = new IntValue(entityConfig.OneWayDamageSpread);
            _delayAfterPerk = entityConfig.DelayAfterPerk;
            Damage = new IntValue(entityConfig.Damage);
            ForcePerk = entityConfig.ForcePerk;
            DurationPerk = new IntValue(entityConfig.DurationPerk);
            _chancePerk = new IntValue(Mathf.Clamp(entityConfig.PercentagesChancePerk, 0, 100));
            _namePerk = entityConfig.TextApplicationsPerk;
            NameCharacter = namesRepository.GetRandomName();
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
            int newHealth = Mathf.Clamp(Health - Math.Abs(value), 0, MaxHealth);
            if (value < 0)
            {
                Debug.LogError("Значение урона отрицательное!");
            }
            
            Health.Set(newHealth);

            OnHarmEnemy?.Invoke(value.ToString(), TypesText.Damage);

            if (Health == 0)
            {
                enemy.Win();
                Destroy();
            }
        }

        public void Destroy()
        {
            StopAttack();
            StopPerk();
            
            OnCharacterDestroy?.Invoke();
        }
        
        public void ChangeDamage(int value)
        {
            value = Math.Max(0, value);
            if (value < 0)
            {
                Debug.LogError("В смену урона входит отрицательное значение!");
            }
            
            Damage.Set(value);
        }

        protected abstract UniTask Perk(Character enemy);
        
        private void Win()
        {
            OnWin?.Invoke(this);
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

                OnHarmEnemy?.Invoke(_namePerk, TypesText.Perk);

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
                if (enemy.Health == 0 || _tokenSourceAttack == null || _tokenSourceAttack.IsCancellationRequested)
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