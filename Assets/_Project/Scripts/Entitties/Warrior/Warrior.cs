using Cysharp.Threading.Tasks;
using GameScene.Level;
using System;

namespace GameScene.Characters.Warrior
{
    public class Warrior : Character
    {
        public Warrior(CharacterConfig entityConfig, string nameEntity)
            : base(entityConfig, nameEntity) 
        { }

        protected override async UniTask Perk(Character enemy)
        {
            enemy.StopAttack();

            await UniTask.Delay(TimeSpan.FromSeconds(DurationPerk), cancellationToken: TokenSourcePerk.Token);

            enemy.StartAttack(enemy);
        }
    }
}