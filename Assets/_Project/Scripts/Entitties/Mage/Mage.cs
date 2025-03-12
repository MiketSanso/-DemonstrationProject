using Cysharp.Threading.Tasks;
using System;
using GameScene.Repositories;
using GameScene.Level.Texts;

namespace GameScene.Characters.Mage
{
    public class Mage : Character
    {
        public Mage(CharacterConfig entityConfig, NamesRepository namesRepository, TextsRepository textsRepository)
            : base(entityConfig, namesRepository, textsRepository)
        { }

        protected override async UniTask Perk(Character enemy)
        {
            int damage = enemy.Damage;
            enemy.ChangeDamage(ForcePerk);

            await UniTask.Delay(TimeSpan.FromSeconds(DurationPerk), cancellationToken: TokenSourcePerk.Token);

            enemy.ChangeDamage(damage);
        }
    }
}
