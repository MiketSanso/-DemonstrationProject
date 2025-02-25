using UnityEngine;


namespace GameScene.Character
{
    [CreateAssetMenu(fileName = "EntityData", menuName = "Entity", order = 51)]
    public class CharacterData : ScriptableObject
    {
        [field: SerializeField]
        public int Damage { get; private set; }

        [field: SerializeField]
        public int Cooldown { get; private set; }

        [field: SerializeField]
        public int OneWayDamageSpread { get; private set; }

        [field: SerializeField]
        public int MaxHealthEntity { get; private set; }

        [field: SerializeField]
        public int DurationPerk { get; private set; }

        [field: SerializeField]
        public int PercentagesChancePerk { get; private set; }

        [field: SerializeField]
        public string TextApplicationsPerk { get; private set; }
    }
}

