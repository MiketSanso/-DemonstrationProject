using Cysharp.Threading.Tasks;
using System;

namespace GameScene.Characters.Warrior
{
    public class Warrior : AttackEnemy
    {
        protected override async UniTask Perk(AttackEnemy enemy, AttackEnemy character)
        {
            enemy.StopTaskAttack();

            await UniTask.Delay(TimeSpan.FromSeconds(character.Character.DurationPerk));

            if (enemy != null)
                enemy.StartTaskAttack(character, enemy);
        }
    }
}