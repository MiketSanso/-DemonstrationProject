<<<<<<< HEAD
using GameScene.Characters;
=======
using GameScene.Character;
using GameScene.HPBars;
>>>>>>> 1ae4312d68fd11c59cbe48c26c578b07b4bdda25
using TMPro;
using UnityEngine;

namespace GameScene.Level
{
    public class CharactersFactory : MonoBehaviour
    {
        [SerializeField]
<<<<<<< HEAD
        private AttackEnemy[] _entitiyPrefabs;
=======
        private CharacterUI[] _entitiyPrefabs;
>>>>>>> 1ae4312d68fd11c59cbe48c26c578b07b4bdda25

        [SerializeField]
        private Transform[] _transformsSpawnEntities = new Transform[2];

        [SerializeField]
        private EndPanelSettings _endPanelSettings;

        [SerializeField]
        private Transform _parentSpawnObjects;

        [SerializeField]
        private HPBar _prefabHPBar;

        [SerializeField]
        private TMP_Text _textPrefab;

<<<<<<< HEAD
        [SerializeField]
        private string[] _namesForEntitys;

        public AttackEnemy[] EntitiesInScene { get; private set; } = new AttackEnemy[2];
=======
        public CharacterUI[] EntitiesInScene { get; private set; } = new CharacterUI[2];
>>>>>>> 1ae4312d68fd11c59cbe48c26c578b07b4bdda25

        public void CreateCharacters()
        {
            for (int i = 0; i < 2; i++)
            {
<<<<<<< HEAD
                AttackEnemy entityForSpawn = _entitiyPrefabs[Random.Range(0, _entitiyPrefabs.Length)];

                AttackEnemy createdObject = Instantiate(entityForSpawn,
=======
                CharacterUI entityForSpawn = _entitiyPrefabs[Random.Range(0, _entitiyPrefabs.Length)];

                CharacterUI createdObject = Instantiate(entityForSpawn,
>>>>>>> 1ae4312d68fd11c59cbe48c26c578b07b4bdda25
                    _transformsSpawnEntities[i].position,
                    Quaternion.identity,
                    _transformsSpawnEntities[i]);
                EntitiesInScene[i] = createdObject;

<<<<<<< HEAD
                string nameEntity = _namesForEntitys[Random.Range(0, _namesForEntitys.Length)];
                Character character = new Character(EntitiesInScene[i].CharacterData, nameEntity);
                EntitiesInScene[i].InitializeVariables(_endPanelSettings, character);

                if (EntitiesInScene[i].TryGetComponent(out CharacterUI characterUI))
                {
                    HPBar hpBarCharacter = CreateHPBar(characterUI, EntitiesInScene[i]);

                    TMP_Text[] poolTextsCharacter = CreatePoolTexts(characterUI, characterUI.CountTextsInPool);

                    characterUI.InitializeVariables(hpBarCharacter, poolTextsCharacter);
                }
                else
                    Debug.LogError("CharacterUI у одного из объектов отсутствует!");
            }

            EntitiesInScene[0].StartTaskAttack(EntitiesInScene[1], EntitiesInScene[0]);
            EntitiesInScene[1].StartTaskAttack(EntitiesInScene[0], EntitiesInScene[1]);
=======
                EntitiesInScene[i].AttackEnemy.InitializeVariables(_endPanelSettings);

                HPBar hpBarCharacter = CreateHPBar(EntitiesInScene[i]);
                TMP_Text[] poolTextsCharacter = CreatePoolTexts(EntitiesInScene[i], EntitiesInScene[i].CountTextsInPool);

                EntitiesInScene[i].InitializeVariables(hpBarCharacter, poolTextsCharacter);
            }

            EntitiesInScene[0].AttackEnemy.StartTaskAttack(EntitiesInScene[1], EntitiesInScene[0]);
            EntitiesInScene[1].AttackEnemy.StartTaskAttack(EntitiesInScene[0], EntitiesInScene[1]);
>>>>>>> 1ae4312d68fd11c59cbe48c26c578b07b4bdda25
        }

        private TMP_Text[] CreatePoolTexts(CharacterUI chareacter, int countTextsInPool)
        {
            TMP_Text[] poolTexts = new TMP_Text[countTextsInPool];

            for (int i = 0; i < countTextsInPool; i++)
            {
                poolTexts[i] = Instantiate(_textPrefab, chareacter.TransformSpawnText.position, Quaternion.identity, _parentSpawnObjects);
            }

            return poolTexts;
        }

<<<<<<< HEAD
        private HPBar CreateHPBar(CharacterUI character, AttackEnemy attackEnemyCharacter)
        {
            HPBar hpBar = Instantiate(_prefabHPBar, character.TransformSpawnHPBar.position, Quaternion.identity, _parentSpawnObjects);
            hpBar.InitializeValues(character.TransformSpawnHPBar, attackEnemyCharacter);
=======
        public HPBar CreateHPBar(CharacterUI chareacter)
        {
            HPBar hpBar = Instantiate(_prefabHPBar, chareacter.TransformSpawnHPBar.position, Quaternion.identity, _parentSpawnObjects);

            hpBar.InitializeValues(chareacter.TransformSpawnHPBar);
>>>>>>> 1ae4312d68fd11c59cbe48c26c578b07b4bdda25

            return hpBar;
        }
    }
}