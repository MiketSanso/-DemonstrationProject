using Cysharp.Threading.Tasks;
using UnityEngine;
using TMPro;
using DG.Tweening;
using Zenject;

namespace GameScene.Character
{
    public class CharacterUI : MonoBehaviour
    {
        public Character Character;

        public TakeDamageEnemy TakeDamageEnemy;

        public CreateHPBarEntity CreateHPBar;

        [HideInInspector]
        public TMP_Text[] _poolTexts;

        [SerializeField]
        protected Transform TransformSpawnText;

        [SerializeField]
        private int _heightFlyText;

        [SerializeField]
        private int _speedFlyText;

        [SerializeField]
        private int _countTextsInPool;

        [SerializeField]
        private CharacterData _characterData;

        [SerializeField]
        private TMP_Text _textPrefab;

        [SerializeField]
        private string[] _namesForEntitys;

        [Inject]
        private Transform _parentText;

        private void Awake()
        {
            CreatePoolTexts();

            Character = new Character(_characterData,
                _namesForEntitys[Random.Range(0, _namesForEntitys.Length)]);
        }

        public void DestroyThisObject()
        {
            Destroy(CreateHPBar.HpBar.gameObject);
            Destroy(gameObject);
        }

        private void CreatePoolTexts()
        {
            _poolTexts = new TMP_Text[_countTextsInPool];

            for (int i = 0; i < _countTextsInPool; i++)
            {
                _poolTexts[i] = Instantiate(_textPrefab, TransformSpawnText.position, Quaternion.identity, _parentText);
            }
        }

        public async UniTask CreateText(string textForSpawn)
        {
            for (int i = 0; i < _countTextsInPool; i++)
            {
                if (_poolTexts[i].color.a == 0)
                {
                    _poolTexts[i].text = textForSpawn;

                    _poolTexts[i].transform.DOMove(new Vector3(transform.position.x, transform.position.y + _heightFlyText), _speedFlyText);
                    _poolTexts[i].DOColor(new Vector4(_textPrefab.color.r, _textPrefab.color.g, _textPrefab.color.b, 0), _speedFlyText);

                    break;
                }
            }
        }
    }
}