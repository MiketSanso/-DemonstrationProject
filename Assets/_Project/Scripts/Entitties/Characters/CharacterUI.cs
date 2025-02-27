using Cysharp.Threading.Tasks;
using UnityEngine;
using TMPro;
using DG.Tweening;

namespace GameScene.Characters
{
    public class CharacterUI : MonoBehaviour
    {
        [SerializeField]
        private int _heightFlyText;

        [SerializeField]
        private int _speedFlyText;

        [SerializeField]
        private AttackEnemy AttackEnemy;

        private TMP_Text[] _poolTexts;

<<<<<<< HEAD
=======
        public HPBar HpBar { get; private set; }

        public Character Character { get; private set; }

        [field: SerializeField]
        public AttackEnemy AttackEnemy { get; private set; }

>>>>>>> 1ae4312d68fd11c59cbe48c26c578b07b4bdda25
        [field: SerializeField]
        public int CountTextsInPool { get; private set; }

        [field: SerializeField]
        public Transform TransformSpawnHPBar { get; private set; }

        [field: SerializeField]
        public Transform TransformSpawnText { get; private set; }
        
        public HPBar HpBar { get; private set; }

        private void OnEnable()
        {
<<<<<<< HEAD
            AttackEnemy.CreateText += StartCreateText;
            AttackEnemy.DestroyCharacter += DestroyThisObject;
        }

        private void OnDisable()
        {
            AttackEnemy.CreateText -= StartCreateText;
            AttackEnemy.DestroyCharacter -= DestroyThisObject;
=======
            Character = new Character(_characterData,
            _namesForEntitys[Random.Range(0, _namesForEntitys.Length)]);
>>>>>>> 1ae4312d68fd11c59cbe48c26c578b07b4bdda25
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

        private async UniTask CreateText(string textForSpawn)
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

        private async void StartCreateText(string textForSpawn)
        {
            await CreateText(textForSpawn);
        }
    }
}