using Cysharp.Threading.Tasks;
using System.Threading;
using GameScene.Level;
using UnityEngine;

namespace GameScene.Character
{
    public abstract class TakeDamageEnemy : MonoBehaviour
    {
        private CalculateDamage calculateDamage = new CalculateDamage();

        protected CancellationTokenSource CtsAttack;

        protected bool PerkIsActive = false;

        [HideInInspector]
        public EndPanelSettings EndPanelSettings;

        public async void StartTaskAttack(CharacterUI enemy, CharacterUI character)
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
            //CtsAttack?.Dispose();
        }

        protected void AddValueToHealthEnemy(int value, CharacterUI enemy)
        {
            enemy.Character.AddingValueToHealth(value);
            enemy.CreateHPBar.HpBar.ChangeHPBar(enemy.Character.HealthEntity.Get(), enemy.Character.MaxHealthEntity.Get());
        }

        private async UniTask TakeDamage(CharacterUI enemy, CharacterUI character)
        {
            do
            {
                if (CtsAttack == null || CtsAttack.IsCancellationRequested)
                {
                    break;
                }

                int damage = calculateDamage.CalculatingDamage(character, character.Character.OneWayDamageSpread.Get());

                AddValueToHealthEnemy(damage, enemy);

                await enemy.CreateText($"{damage} HP");

                CheckHPEnemy(enemy);

                await UniTask.Delay(character.Character.Cooldown.Get() * 1000);

                if (Random.Range(0, 101) < character.Character.PercentagesChancePerk.Get()
                    && !PerkIsActive
                    && enemy != null)
                {
                    UsePerk(enemy, character);
                }

                await UniTask.Delay(500);

            } while (enemy != null);
        }

        private void CheckHPEnemy(CharacterUI enemy)
        {
            if (enemy.Character.HealthEntity.Get() == 0)
            {
                enemy.DestroyThisObject();
                EndPanelSettings.ActivateEndPanel(enemy.Character.TextNameEntity);

                StopTaskAttack();
            }
        }

        private async void UsePerk(CharacterUI enemy, CharacterUI character)
        {
            await character.CreateText($"Персонаж использовал перк: {character.Character.TextApplicationsPerk}");

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

        protected abstract UniTask Perk(CharacterUI enemy, CharacterUI character);
    }
}