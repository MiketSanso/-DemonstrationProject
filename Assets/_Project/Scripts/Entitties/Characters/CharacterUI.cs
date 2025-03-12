using UnityEngine;
using GameScene.Level.Texts;
using TMPro;
using DG.Tweening;

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
            _character.OnUsePerk -= RespawnText;
            _character.OnDamaged -= RespawnText;
            _character.OnCharacterDestroy -= Destroy;
        }

        public void Initialize(HpBar hpBar, PoolEffectTexts poolTexts, Character character)
        {
            _hpBar = hpBar;
            _poolTexts = poolTexts;
            _character = character;

            _character.OnUsePerk += RespawnText;
            _character.OnDamaged += RespawnText;
            _character.OnCharacterDestroy += Destroy;
        }

        private void Destroy()
        {
            _hpBar.Destroy();
            
            _poolTexts.DestroyPool();

            Destroy(gameObject);
        }

        private void RespawnText(string textForSpawn)
        {
            foreach (TMP_Text text in _poolTexts.Texts)
            {
                if (text.color.a == 0)
                {
                    text.color = new Vector4(text.color.r, text.color.g, text.color.b, 100);
                    text.transform.position = TransformSpawnText.position;

                    text.text = textForSpawn;

                    text.transform.DOMove(new Vector3(text.transform.position.x, text.transform.position.y + _heightFlyText), _speedFlyText);
                    text.DOColor(new Vector4(text.color.r, text.color.g, text.color.b, 0), _speedFlyText);
                }
            }
        }
    }
}