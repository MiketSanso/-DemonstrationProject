using Cysharp.Threading.Tasks;
using System;
using GameScene.Repositories;

namespace GameScene.Characters.Mage
{
    public class Mage : Character
    {
        public Mage(CharacterConfig entityConfig, NamesRepository namesRepository)
            : base(entityConfig, namesRepository)
        { }

        protected override async UniTask Perk(Character enemy)
        {
            int damage = enemy.Damage;
            enemy.ChangeDamage();

            await UniTask.Delay(TimeSpan.FromSeconds(DurationPerk), cancellationToken: TokenSourcePerk.Token);

            enemy.ChangeDamage(damage);
        }
    }
}
