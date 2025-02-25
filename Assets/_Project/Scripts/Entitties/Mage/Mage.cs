using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GameScene.Character.Mage
{
    public class Mage : AttackEnemy
    {
        [SerializeField]
        private int _coefChangeDamage;

        protected override async UniTask Perk(CharacterUI enemy, CharacterUI character)
        {
            enemy.Character.ChangeCoefChangeDamage(_coefChangeDamage);

            await UniTask.Delay(character.Character.DurationPerk.Get() * 1000);

            enemy.Character.ChangeCoefChangeDamage(0);
        }
    }
}
