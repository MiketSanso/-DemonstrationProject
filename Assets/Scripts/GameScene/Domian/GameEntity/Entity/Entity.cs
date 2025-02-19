using UnityEngine;

public class Entity
{
    public int BaseDamage { get; private set; }

    public int Cooldown { get; private set; }

    public int HealthEntity { get; private set; }

    public int MaxHealthEntity { get; private set; }

    public EntityType EntityType { get; private set; }

    public Entity(int healthEntity, int maxHealthEntity, int cooldown, int baseDamage, EntityType entityType)
    {
        HealthEntity = healthEntity;
        MaxHealthEntity = maxHealthEntity;
        Cooldown = cooldown;
        EntityType = entityType;
        BaseDamage = baseDamage;
    }

    public void AddingValueToHealth(int valueChange)
    {
        HealthEntity = Mathf.Clamp(HealthEntity + valueChange, 0, MaxHealthEntity);
    }
}

public enum EntityType
{
    Warrior,
    Mage,
    Archer
}
