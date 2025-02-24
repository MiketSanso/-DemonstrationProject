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

        [SerializeField]
        private int _heightFlyText;

        [SerializeField]
        private int _speedFlyText;

        [SerializeField]
        private CharacterData _characterData;

        [SerializeField]
        protected Transform TransformSpawnText;

        [SerializeField]
        private TMP_Text _textPrefab;

        [SerializeField]
        private string[] _namesForEntitys;

        [Inject]
        private Transform _parentText;

        private void Awake()
        {
            Character = new Character(_characterData,
                _namesForEntitys[Random.Range(0, _namesForEntitys.Length)]);
        }

        public void DestroyThisObject()
        {
            Destroy(CreateHPBar.HpBar.gameObject);
            Destroy(gameObject);
        }

        public async UniTask CreateText(string textForSpawn)
        {
            TMP_Text text = Instantiate(_textPrefab, TransformSpawnText.position, Quaternion.identity, _parentText);
            text.text = textForSpawn;

            text.transform.DOMove(new Vector3(transform.position.x, transform.position.y + _heightFlyText), _speedFlyText);
            text.DOColor(new Vector4(_textPrefab.color.r, _textPrefab.color.g, _textPrefab.color.b, 0), _speedFlyText);
        }
    }
}