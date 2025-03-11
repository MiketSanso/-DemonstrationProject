using UnityEngine;
using GameScene.Level.Texts;

namespace GameScene.Characters
{
    public class CharacterUI : MonoBehaviour
    {
        [SerializeField] private int _heightFlyText;
        [SerializeField] private int _speedFlyText;
        
        private Character _character;
        private PoolEffectTexts _poolTexts;
        private HpBar _hpBar;

        [field: SerializeField] public int SizePool { get; private set; }
        [field: SerializeField] public Transform TransformSpawnHpBar { get; private set; }
        [field: SerializeField] public Transform TransformSpawnText { get; private set; }

        private void OnDisable()
        {
            _character.OnHarmEnemy -= CreateText;
            _character.OnCharacterDestroy -= Destroy;
        }

        public void Initialize(HpBar hpBar, PoolEffectTexts poolTexts, Character character)
        {
            _hpBar = hpBar;
            _poolTexts = poolTexts;
            _character = character;

            _character.OnHarmEnemy += CreateText;
            _character.OnCharacterDestroy += Destroy;
        }

        private void Destroy()
        {
            _hpBar.Destroy();
            
            _poolTexts.DestroyPool();

            Destroy(gameObject);
        }

        private void CreateText(string textForSpawn, TypesText textType)
        {
            _poolTexts.RespawnText(textType, textForSpawn, TransformSpawnHpBar, _heightFlyText, _speedFlyText);
        }
    }
}