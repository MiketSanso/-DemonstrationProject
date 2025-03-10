using Cysharp.Threading.Tasks;
using GameScene.Level;
using System;
using GameScene.Repositories;

namespace GameScene.Characters.Archer
{
    public class Archer : Character
    {
        public Archer(CharacterConfig entityConfig, NamesRepository namesRepository)
            : base(entityConfig, namesRepository)
        { }

        protected override async UniTask Perk(Character enemy)
        {
            int timeToxicDamage = DurationPerk;

            do
            {
                if (enemy.HealthCharacter == 0)
                    break;

                enemy.GetDamage(Damage, enemy);

                await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: TokenSourcePerk.Token);

                timeToxicDamage -= 1;
            } while (timeToxicDamage != 0 && TokenSourcePerk != null && !TokenSourcePerk.IsCancellationRequested);

            IsPerkActive = false;
        }
    }
}
