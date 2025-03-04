using Cysharp.Threading.Tasks;
using GameScene.Level;
using System;
using UnityEngine;

namespace GameScene.Characters.Archer
{
    public class Archer : Character
    {
        public Archer(CharacterScriptableData entityData, string nameEntity, EndPanel endPanelSettings)
            : base(entityData, nameEntity, endPanelSettings)
        { }

        protected override async UniTask Perk(Character enemy)
        {
            int timeToxicDamage = DurationPerk;

            do
            {
                if (enemy.HealthEntity == 0)
                    break;

                enemy.TakeDamage(-BaseDamage);

                await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: CtsPerk.Token);

                timeToxicDamage -= 1;
            } while (timeToxicDamage != 0 && CtsPerk != null && !CtsPerk.IsCancellationRequested);

            IsPerkActive = false;
        }
    }
}
