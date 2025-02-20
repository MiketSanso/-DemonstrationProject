using UnityEngine;


namespace GameScene.Entity
{
    [CreateAssetMenu(fileName = "EntityData", menuName = "Entity", order = 51)]
    public class EntityData : ScriptableObject
    {
        [SerializeField]
        private int _damage;

        [SerializeField]
        private int _cooldown;

        [SerializeField]
        private int _maxHealthEntity;

        [SerializeField]
        private int _durationPerk;

        [SerializeField]
        private int _percentagesChancePerk;

        [SerializeField]
        private string _textApplicationsPerk;

        [SerializeField]
        private EntityType _entityType;


        public int BaseDamage => _damage;
        public int Cooldown => _cooldown;
        public int MaxHealthEntity => _maxHealthEntity;
        public int DurationPerk => _durationPerk;
        public int PercentagesChancePerk => _percentagesChancePerk;
        public string TextApplicationsPerk => _textApplicationsPerk;
        public EntityType EntityType => _entityType;
    }
}

