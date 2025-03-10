using UnityEngine;

namespace GameScene.Characters
{
    [CreateAssetMenu(fileName = "CharacterConfig", menuName = "Character", order = 1)]
    public class CharacterConfig : ScriptableObject
    {
        [field: SerializeField] public int Damage { get; private set; }
        
        [field: SerializeField] public int ForcePerk  { get; private set; }

        [field: SerializeField] public int Cooldown { get; private set; }

        [field: SerializeField] public int OneWayDamageSpread { get; private set; }

        [field: SerializeField] public int MaxHealthEntity { get; private set; }

        [field: SerializeField] public int DurationPerk { get; private set; }
        
        [field: SerializeField] public int PercentagesChancePerk { get; private set; }
        
        [field: SerializeField] public float DelayAfterPerk { get; private set; }

        [field: SerializeField] public string TextApplicationsPerk { get; private set; }

        [field: SerializeField] public Color Color { get; private set; }

        [field: SerializeField] public CharacterType CharacterType { get; private set; }
    }
}

