using Cysharp.Threading.Tasks;
using System;
using GameScene.Repositories;

namespace GameScene.Characters.Warrior
{
    public class Warrior : Character
    {
        public Warrior(CharacterConfig entityConfig, NamesRepository namesRepository)
            : base(entityConfig, namesRepository) 
        { }

        protected override async UniTask Perk(Character enemy)
        {
            int damage = enemy.Damage;
            enemy.ChangeDamage(0);

            await UniTask.Delay(TimeSpan.FromSeconds(DurationPerk * ForcePerk), cancellationToken: TokenSourcePerk.Token);

            enemy.ChangeDamage(damage);
        }
    }
}