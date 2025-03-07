using Cysharp.Threading.Tasks;
using GameScene.Level;
using System;
using UnityEngine;

namespace GameScene.Characters.Mage
{
    public class Mage : Character
    {
        public Mage(CharacterConfig entityConfig, string nameEntity)
            : base(entityConfig, nameEntity)
        { }

        protected override async UniTask Perk(Character enemy)
        {
            enemy.ChangeCoefDamage();

            await UniTask.Delay(TimeSpan.FromSeconds(DurationPerk), cancellationToken: TokenSourcePerk.Token);

            enemy.ChangeCoefDamage(0);
        }
    }
}
