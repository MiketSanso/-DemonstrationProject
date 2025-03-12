using Cysharp.Threading.Tasks;
using System;
using GameScene.Repositories;
using GameScene.Level.Texts;

namespace GameScene.Characters.Warrior
{
    public class Warrior : Character
    {
        public Warrior(CharacterConfig entityConfig, NamesRepository namesRepository, TextsRepository textsRepository)
            : base(entityConfig, namesRepository, textsRepository) 
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