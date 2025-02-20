using System.Threading.Tasks;
using UnityEngine;

namespace GameScene.Entity.Archer
{
    public class Archer : EntityUI
    {
        [SerializeField]
        private int _toxicDamage;

        protected override async void UseEntityPerk(EntityUI enemy)
        {
            base.UseEntityPerk(enemy);

            await TakeDamageToxic();
        }

        private async Task TakeDamageToxic()
        {
            int timeToxicDamage = _entity.DurationPerk;
            do
            {
                int indexSelectedEntity = Random.Range(0, _entitiesInAttaksZone.Count);

                _entitiesInAttaksZone[indexSelectedEntity].AddingValueToHealth(-_toxicDamage);

                await Task.Delay(1000);

                timeToxicDamage -= 1;
            } while (_entitiesInAttaksZone.Count != 0);

            _perkIsActive = false;
        }
    }
}
