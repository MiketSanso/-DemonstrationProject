using Cysharp.Threading.Tasks;
using System;

namespace GameScene.Characters.Warrior
{
    public class Warrior : Character
    {
        protected override async UniTask Perk(Character enemy)
        {
            enemy.StopAttack();

            await UniTask.Delay(TimeSpan.FromSeconds(DurationPerk));

            if (enemy != null)
                enemy.StartAttack(enemy);
        }
    }
}