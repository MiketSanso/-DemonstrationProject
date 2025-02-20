using System.Threading.Tasks;
using UnityEngine;

namespace GameScene.Entity.Mage
{
    public class Mage : EntityUI
    {
        [SerializeField]
        private int _coefChangeDamage;

        protected override async void UseEntityPerk(EntityUI enemy)
        {
            base.UseEntityPerk(enemy);

            await ChangeDamageEnemy(enemy);
        }

        private async Task ChangeDamageEnemy(EntityUI enemy)
        {
            enemy._entity.ChangeCoefChangeDamage(_coefChangeDamage);

            await Task.Delay(_entity.DurationPerk * 1000);

            enemy._entity.ChangeCoefChangeDamage(0);

            _perkIsActive = false;
        }
    }
}
