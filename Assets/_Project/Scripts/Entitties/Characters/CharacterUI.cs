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
        
        private TextsRepository _textsRepository;
        private Character _character;
        private PoolEffectTexts _poolTexts;
        private HpBar _hpBar;

        [field: SerializeField] public int SizePool { get; private set; }
        [field: SerializeField] public Transform TransformSpawnHpBar { get; private set; }
        [field: SerializeField] public Transform TransformSpawnText { get; private set; }

        private void OnDisable()
        {
            _character.OnHarmEnemy -= RespawnText;
            _character.OnCharacterDestroy -= Destroy;
        }

        public void Initialize(HpBar hpBar, PoolEffectTexts poolTexts, Character character, TextsRepository textsRepository)
        {
            _hpBar = hpBar;
            _poolTexts = poolTexts;
            _character = character;
            _textsRepository = textsRepository;

            _character.OnHarmEnemy += RespawnText;
            _character.OnCharacterDestroy += Destroy;
        }

        private void Destroy()
        {
            _hpBar.Destroy();
            
            _poolTexts.DestroyPool();

            Destroy(gameObject);
        }

        public void RespawnText(string textForSpawn, TypesText typeText)
        {
            foreach (TMP_Text text in _poolTexts.Texts)
            {
                if (text.color.a == 0)
                {
                    text.color = new Vector4(text.color.r, text.color.g, text.color.b, 100);
                    text.transform.position = TransformSpawnText.position;

                    if (typeText == TypesText.Damage)
                    {
                        text.text = $"-{textForSpawn}";
                    }
                    else if (typeText == TypesText.Perk)
                    {
                        text.text = $"{_textsRepository.UsePerk} {textForSpawn}";
                    }

                    text.transform.DOMove(new Vector3(text.transform.position.x, text.transform.position.y + _heightFlyText), _speedFlyText);
                    text.DOColor(new Vector4(text.color.r, text.color.g, text.color.b, 0), _speedFlyText);
                }
            }
        }
    }
}