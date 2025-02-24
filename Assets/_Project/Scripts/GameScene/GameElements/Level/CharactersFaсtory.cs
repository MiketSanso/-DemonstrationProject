using GameScene.Character;
using GameScene.HPBars;
using TMPro;
using UnityEngine;

namespace GameScene.Level
{
    public class CharactersFañtory : MonoBehaviour
    {
        [SerializeField]
        private CharacterUI[] _entitiyPrefabs;

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

        public CharacterUI[] EntitiesInScene { get; private set; } = new CharacterUI[2];

        public void CreateCharacters()
        {
            for (int i = 0; i < 2; i++)
            {
                CharacterUI entityForSpawn = _entitiyPrefabs[Random.Range(0, _entitiyPrefabs.Length)];

                CharacterUI createdObject = Instantiate(entityForSpawn,
                    _transformsSpawnEntities[i].position,
                    Quaternion.identity,
                    _transformsSpawnEntities[i]);
                EntitiesInScene[i] = createdObject;

                EntitiesInScene[i].TakeDamageEnemy.InitializeVariables(_endPanelSettings);

                HPBar hpBarCharacter = CreateHPBar(EntitiesInScene[i]);
                TMP_Text[] poolTextsCharacter = CreatePoolTexts(EntitiesInScene[i], EntitiesInScene[i].CountTextsInPool);

                EntitiesInScene[i].InitializeVariables(hpBarCharacter, poolTextsCharacter);
            }

            EntitiesInScene[0].TakeDamageEnemy.StartTaskAttack(EntitiesInScene[1], EntitiesInScene[0]);
            EntitiesInScene[1].TakeDamageEnemy.StartTaskAttack(EntitiesInScene[0], EntitiesInScene[1]);
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

        public HPBar CreateHPBar(CharacterUI chareacter)
        {
            HPBar hpBar = Instantiate(_prefabHPBar, chareacter.TransformSpawnHPBar.position, Quaternion.identity, _parentSpawnObjects);

            hpBar.InitializeValues(chareacter.TransformSpawnHPBar);

            return hpBar;
        }
    }
}