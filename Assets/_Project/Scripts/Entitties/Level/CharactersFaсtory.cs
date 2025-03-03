using GameScene.Characters;
using TMPro;
using UnityEngine;

namespace GameScene.Level
{
    public class CharactersFactory : MonoBehaviour
    {
        [SerializeField]
        private Character[] _entitiyPrefabs;

        [SerializeField]
        private Transform[] _transformsSpawnEntities = new Transform[2];

        [SerializeField]
        private EndPanel _endPanelSettings;

        [SerializeField]
        private Transform _parentSpawnObjects;

        [SerializeField]
        private HPBar _prefabHPBar;

        [SerializeField]
        private TMP_Text _textPrefab;

        [SerializeField]
        private string[] _namesForEntitys;

        public Character[] EntitiesInScene { get; private set; } = new Character[2];

        public void CreateCharacters()
        {
            for (int i = 0; i < 2; i++)
            {
                Character entityForSpawn = _entitiyPrefabs[Random.Range(0, _entitiyPrefabs.Length)];

                Character createdObject = Instantiate(entityForSpawn,
                    _transformsSpawnEntities[i].position,
                    Quaternion.identity,
                    _transformsSpawnEntities[i]);
                EntitiesInScene[i] = createdObject;

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

        private HPBar CreateHPBar(CharacterUI character, AttackEnemy attackEnemyCharacter)
        {
            HPBar hpBar = Instantiate(_prefabHPBar, character.TransformSpawnHPBar.position, Quaternion.identity, _parentSpawnObjects);
            hpBar.InitializeValues(character.TransformSpawnHPBar, attackEnemyCharacter);

            return hpBar;
        }
    }
}