using Cysharp.Threading.Tasks;
using GameScene.Level;
using System;
using UnityEngine;

namespace GameScene.Characters.Archer
{
    public class Archer : Character
    {
        [SerializeField]
        private int _toxicDamage;

        public Archer(CharacterScriptableData entityData, string nameEntity, EndPanel endPanelSettings) 
            : base(entityData, nameEntity, endPanelSettings)
        {
        }

        protected override async UniTask Perk(Character enemy)
        {
            int timeToxicDamage = DurationPerk;

            do
            {
                enemy.TakeDamage(-_toxicDamage);

                await UniTask.Delay(TimeSpan.FromSeconds(1));

                timeToxicDamage -= 1;
            } while (timeToxicDamage != 0);

            IsPerkActive = false;
        }
    }
}
