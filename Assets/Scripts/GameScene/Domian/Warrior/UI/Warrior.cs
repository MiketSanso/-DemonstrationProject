using System.Threading.Tasks;
using UnityEngine;

namespace GameScene.Entity.Warrior
{
    public class Warrior : EntityUI
    {
        protected override async void UseEntityPerk(EntityUI enemy)
        {
            base.UseEntityPerk(enemy);

            await StunEnemy();
        }

        private async Task StunEnemy()
        {
            EntityUI entity = _entitiesInAttaksZone[Random.Range(0, _entitiesInAttaksZone.Count)];

            entity.StopTaskAttack();

            await Task.Delay(_entity.DurationPerk * 1000);

            if (entity != null)
                entity.StartTaskAttack();

            _perkIsActive = false;
        }
    }
}