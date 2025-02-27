using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace GameScene.Characters.Archer
{
    public class Archer : AttackEnemy
    {
        [SerializeField]
        private int _toxicDamage;

        protected override async UniTask Perk(AttackEnemy enemy, AttackEnemy character)
        {
            int timeToxicDamage = character.Character.DurationPerk;

            do
            {
                AddValueToHealthEnemy(-_toxicDamage, enemy);

                await UniTask.Delay(TimeSpan.FromSeconds(1));

                timeToxicDamage -= 1;
            } while (timeToxicDamage != 0);

            PerkIsActive = false;
        }
    }
}
