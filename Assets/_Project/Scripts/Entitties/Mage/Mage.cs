using Cysharp.Threading.Tasks;
using GameScene.Level;
using System;
using UnityEngine;

namespace GameScene.Characters.Mage
{
    public class Mage : Character
    {
        public Mage(CharacterScriptableData entityData, string nameEntity, EndPanel endPanelSettings)
            : base(entityData, nameEntity, endPanelSettings)
        { }

        [SerializeField]
        private int _coefChangeDamage;

        protected override async UniTask Perk(Character enemy)
        {
            enemy.ChangeCoefChangeDamage(_coefChangeDamage);

            await UniTask.Delay(TimeSpan.FromSeconds(DurationPerk), cancellationToken: CtsPerk.Token);

            enemy.ChangeCoefChangeDamage(0);
        }
    }
}
