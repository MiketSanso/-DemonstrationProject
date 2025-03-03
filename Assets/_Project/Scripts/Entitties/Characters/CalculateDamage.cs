using UnityEngine;

namespace GameScene.Characters
{
    public class CalculateDamage
    {
        public int CalculatingDamage(Character character, int oneWayDamageSpread)
        {
            int damage;

            if (character.BaseDamage == 0)
            {
                damage = 0;
            }
            else
            {
                damage = Random.Range(-character.BaseDamage - oneWayDamageSpread, -character.BaseDamage + oneWayDamageSpread) + character.CoefChangeDamage;
                if (damage > 0)
                    damage = 0;
            }

            return damage;
        }
    }
}