using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace GameScene.Characters.Mage
{
    public class Mage : AttackEnemy
    {
        [SerializeField]
        private int _coefChangeDamage;

        protected override async UniTask Perk(AttackEnemy enemy, AttackEnemy character)
        {
            enemy.Character.ChangeCoefChangeDamage(_coefChangeDamage);

            await UniTask.Delay(TimeSpan.FromSeconds(character.Character.DurationPerk));

            enemy.Character.ChangeCoefChangeDamage(0);
        }
    }
}
