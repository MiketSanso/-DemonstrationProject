using Cysharp.Threading.Tasks;
using UnityEngine;
using TMPro;
using DG.Tweening;
using GameScene.HPBars;

namespace GameScene.Character
{
    public class CharacterUI : MonoBehaviour
    {
        [SerializeField]
        private int _heightFlyText;

        [SerializeField]
        private int _speedFlyText;

        [SerializeField]
        private CharacterData _characterData;

        [SerializeField]
        private string[] _namesForEntitys;

        private TMP_Text[] _poolTexts;

        public HPBar HpBar { get; private set; }

        public Character Character { get; private set; }

        [field: SerializeField]
        public AttackEnemy AttackEnemy { get; private set; }

        [field: SerializeField]
        public int CountTextsInPool { get; private set; }

        [field: SerializeField]
        public Transform TransformSpawnHPBar { get; private set; }

        [field: SerializeField]
        public Transform TransformSpawnText { get; private set; }

        private void Awake()
        {
            Character = new Character(_characterData,
            _namesForEntitys[Random.Range(0, _namesForEntitys.Length)]);
        }

        public void InitializeVariables(HPBar hpBar, TMP_Text[] poolTexts)
        {
            HpBar = hpBar;
            _poolTexts = poolTexts;
        }

        public void DestroyThisObject()
        {
            HpBar.DestroyHPBar();
            AttackEnemy.StopTaskAttack();

            for (int i = 0; i < CountTextsInPool; i++)
            {
                Destroy(_poolTexts[i].gameObject);
            }

            Destroy(gameObject);
        }

        public async UniTask CreateText(string textForSpawn)
        {
            for (int i = 0; i < CountTextsInPool; i++)
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