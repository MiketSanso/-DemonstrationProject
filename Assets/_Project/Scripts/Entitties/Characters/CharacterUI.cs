using Cysharp.Threading.Tasks;
using UnityEngine;
using TMPro;
using DG.Tweening;
using GameScene.Level;

namespace GameScene.Characters
{
    public class CharacterUI : MonoBehaviour
    {
        [SerializeField] private int _heightFlyText;

        [SerializeField] private int _speedFlyText;
        
        private Character _character;

        private TMP_Text[] _poolTexts;

        private HPBar _hpBar;

        [field: SerializeField] public int SizePool { get; private set; }

        [field: SerializeField] public Transform TransformSpawnHPBar { get; private set; }

        [field: SerializeField] public Transform TransformSpawnText { get; private set; }

        private void OnDisable()
        {
            _character.OnHarmEnemy -= CreateText;
            _character.OnCharacterDestroy -= Destroy;
        }

        public void Initialize(HPBar hpBar, TMP_Text[] poolTexts, Character character)
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

            for (int i = 0; i < SizePool; i++)
            {
                Destroy(_poolTexts[i].gameObject);
            }

            Destroy(gameObject);
        }

        private async void CreateText(string textForSpawn)
        {
            await TaskCreateText(textForSpawn);
        }

        private async UniTask TaskCreateText(string textForSpawn)
        {
            for (int i = 0; i < SizePool; i++)
            {
                if (_poolTexts[i].color.a == 0)
                {
                    _poolTexts[i].color = new Vector4(_poolTexts[i].color.r, _poolTexts[i].color.g, _poolTexts[i].color.b, 100);
                    _poolTexts[i].transform.position = TransformSpawnText.position;
                    _poolTexts[i].text = textForSpawn;

                    _poolTexts[i].transform.DOMove(new Vector3(_poolTexts[i].transform.position.x, _poolTexts[i].transform.position.y + _heightFlyText), _speedFlyText);
                    _poolTexts[i].DOColor(new Vector4(_poolTexts[i].color.r, _poolTexts[i].color.g, _poolTexts[i].color.b, 0), _speedFlyText);

                    break;
                }
            }
        }
    }
}