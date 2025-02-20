using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

namespace GameScene.Entity
{
    public class Entity
    {
        private int _baseDamage;
        public int BaseDamage
        {
            get { return _baseDamage; }
            private set { ChangeIntValue(ref _baseDamage, value); }
        }

        private int _coefChangeDamage;
        public int CoefChangeDamage
        {
            get { return _coefChangeDamage; }
            private set { ChangeIntValue(ref _coefChangeDamage, value); }
        }

        private int _cooldown;
        public int Cooldown
        {
            get { return _cooldown; }
            private set { ChangeIntValue(ref _cooldown, value); }
        }

        private int _healthEntity;
        public int HealthEntity
        {
            get { return _healthEntity; }
            private set { ChangeIntValue(ref _healthEntity, value); }
        }

        private int _maxHealthEntity;
        public int MaxHealthEntity
        {
            get { return _maxHealthEntity; }
            private set { ChangeIntValue(ref _maxHealthEntity, value); }
        }

        private int _durationPerk;
        public int DurationPerk
        {
            get { return _durationPerk; }
            private set { ChangeIntValue(ref _durationPerk, value); }
        }

        private int _percentagesChancePerk;
        public int PercentagesChancePerk
        {
            get { return _percentagesChancePerk; }
            private set
            {
                _percentagesChancePerk = Mathf.Clamp(value, 0, 100);
            }
        }

        public string TextApplicationsPerk { get; private set; }

        public string TextNameEntity { get; private set; }

        public EntityType EntityType { get; private set; }

        public Entity(int healthEntity, int maxHealthEntity, int cooldown, int baseDamage, int durationPerk, int percentagesChancePerk, string textApplicationsPerk, string textNameEntity, EntityType entityType)
        {
            CoefChangeDamage = 0;
            HealthEntity = healthEntity;
            MaxHealthEntity = maxHealthEntity;
            Cooldown = cooldown;
            BaseDamage = baseDamage;
            DurationPerk = durationPerk;
            PercentagesChancePerk = percentagesChancePerk;
            TextApplicationsPerk = textApplicationsPerk;
            TextNameEntity = textNameEntity;
            EntityType = entityType;
        }

        private void ChangeIntValue(ref int changedValue, int newValue)
        {
            if (newValue > 0)
                changedValue = newValue;
            else
                changedValue = 0;
        }

        public void AddingValueToHealth(int valueChange)
        {
            HealthEntity = Mathf.Clamp(HealthEntity + valueChange, 0, MaxHealthEntity);
        }

        public void ChangeCoefChangeDamage(int newValue)
        {
            if (newValue > 0)
                CoefChangeDamage = newValue;
            else
                CoefChangeDamage = 0;
        }
    }

    public enum EntityType
    {
        Warrior,
        Mage,
        Archer
    }

}
