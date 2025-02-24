using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GameScene.Character.Archer
{
    public class Archer : TakeDamageEnemy
    {
        [SerializeField]
        private int _toxicDamage;

        protected override async UniTask Perk(CharacterUI enemy, CharacterUI character)
        {
            int timeToxicDamage = character.Character.DurationPerk.Get();

            do
            {
                AddValueToHealthEnemy(-_toxicDamage, enemy);

                await UniTask.Delay(1000);

                timeToxicDamage -= 1;
            } while (enemy != null);

            PerkIsActive = false;
        }
    }
}
