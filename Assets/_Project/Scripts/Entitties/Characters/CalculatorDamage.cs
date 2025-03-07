using UnityEngine;

namespace GameScene.Characters
{
    public class CalculatorDamage
    {
        public int Calculate(Character character, int oneWayDamageSpread)
        {
            int damage;

            if (character.Damage == 0)
            {
                damage = 0;
            }
            else
            {
                damage = Random.Range(-character.Damage - oneWayDamageSpread, -character.Damage + oneWayDamageSpread) + character.CoefChangeDamage;
                if (damage > 0)
                    damage = 0;
            }

            return damage;
        }
    }
}