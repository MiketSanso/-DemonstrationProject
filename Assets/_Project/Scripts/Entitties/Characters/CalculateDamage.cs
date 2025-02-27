using UnityEngine;

namespace GameScene.Characters
{
    public class CalculateDamage
    {
        public int CalculatingDamage(AttackEnemy character, int oneWayDamageSpread)
        {
            int damage;

            if (character.Character.BaseDamage == 0)
            {
                damage = 0;
            }
            else
            {
                damage = Random.Range(-character.Character.BaseDamage - oneWayDamageSpread, -character.Character.BaseDamage + oneWayDamageSpread) + character.Character.CoefChangeDamage;
                if (damage > 0)
                    damage = 0;
            }

            return damage;
        }
    }
}