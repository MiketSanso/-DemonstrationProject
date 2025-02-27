using System.Threading;
using UnityEngine;

namespace GameScene.Characters
{
    public class Character
    {
        private CharacterData _entityData;
        public IntValue BaseDamage { get; private set; }
        public IntValue CoefChangeDamage { get; private set; }
        public IntValue Cooldown { get; private set; }
        public IntValue OneWayDamageSpread { get; private set; }
        public IntValue HealthEntity { get; private set; }
        public IntValue MaxHealthEntity { get; private set; }
        public IntValue DurationPerk { get; private set; }
        public IntValue PercentagesChancePerk { get; private set; }

        public string TextApplicationsPerk { get; private set; }

        public string TextNameEntity { get; private set; }

        protected CancellationTokenSource Cts;

        public Character(CharacterData entityData, string nameEntity)
        {
            _entityData = entityData;
            InitializeElements(nameEntity);
        }

        private void InitializeElements(string nameEntity)
        {
            CoefChangeDamage = new IntValue(0);
            HealthEntity = new IntValue(_entityData.MaxHealthEntity);
            MaxHealthEntity = new IntValue(_entityData.MaxHealthEntity);
            Cooldown = new IntValue(_entityData.Cooldown);
            OneWayDamageSpread = new IntValue(_entityData.OneWayDamageSpread);
            BaseDamage = new IntValue(_entityData.Damage);
            DurationPerk = new IntValue(_entityData.DurationPerk);
            PercentagesChancePerk = new IntValue(Mathf.Clamp(_entityData.PercentagesChancePerk, 0, 100));
            TextApplicationsPerk = _entityData.TextApplicationsPerk;
            TextNameEntity = nameEntity;
        }

        public void AddingValueToHealth(int valueChange)
        {
            HealthEntity = new IntValue(Mathf.Clamp(HealthEntity + valueChange, 0, MaxHealthEntity));
        }

        public void ChangeCoefChangeDamage(int newValue)
        {
            if (newValue > 0)
                CoefChangeDamage = new IntValue(newValue);
            else
                CoefChangeDamage = new IntValue(0);
        }
    }

    public readonly struct IntValue
    {
        private readonly int _integer;

        public IntValue(int integer)
        {
            if (integer > 0)
                _integer = integer;
            else
                _integer = 0;
        }

        public static implicit operator int(IntValue d) => d._integer;
        public static explicit operator IntValue(byte b) => new IntValue(b);
    }
}
