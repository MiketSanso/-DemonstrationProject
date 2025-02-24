using GameScene.Character;
using UnityEngine;

namespace GameScene.Character
{
    public class CalculateDamage
    {
        public int CalculatingDamage(CharacterUI character, int oneWayDamageSpread)
        {
            int damage;

            if (character.Character.BaseDamage.Get() == 0)
            {
                damage = 0;
            }
            else
            {
                damage = Random.Range(-character.Character.BaseDamage.Get() - oneWayDamageSpread, -character.Character.BaseDamage.Get() + oneWayDamageSpread) + character.Character.CoefChangeDamage.Get();
                if (damage > 0)
                    damage = 0;
            }

            return damage;
        }
    }
}