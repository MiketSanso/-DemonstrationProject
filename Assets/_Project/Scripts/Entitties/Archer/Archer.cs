using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace GameScene.Characters.Archer
{
    public class Archer : Character
    {
        [SerializeField]
        private int _toxicDamage;

        protected override async UniTask Perk(Character enemy)
        {
            int timeToxicDamage = DurationPerk;

            do
            {
                enemy.TakeDamage(-_toxicDamage);

                await UniTask.Delay(TimeSpan.FromSeconds(1));

                timeToxicDamage -= 1;
            } while (timeToxicDamage != 0);

            IsPerkActive = false;
        }
    }
}
