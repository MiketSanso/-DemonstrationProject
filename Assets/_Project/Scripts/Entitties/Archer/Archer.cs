using Cysharp.Threading.Tasks;
using System;
using GameScene.Repositories;
using GameScene.Level.Texts;

namespace GameScene.Characters.Archer
{
    public class Archer : Character
    {
        public Archer(CharacterConfig entityConfig, NamesRepository namesRepository, TextsRepository textsRepository)
            : base(entityConfig, namesRepository, textsRepository)
        { }

        protected override async UniTask Perk(Character enemy)
        {
            int timeToxicDamage = DurationPerk;

            do
            {
                if (enemy.Health == 0)
                    break;

                enemy.GetDamage(ForcePerk, enemy);

                await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: TokenSourcePerk.Token);

                timeToxicDamage -= 1;
            } while (timeToxicDamage != 0 && TokenSourcePerk != null && !TokenSourcePerk.IsCancellationRequested);

            IsPerkActive = false;
        }
    }
}
