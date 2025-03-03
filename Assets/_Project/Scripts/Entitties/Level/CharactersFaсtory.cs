using GameScene.Characters;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace GameScene.Level
{
    public class CharactersFactory : MonoBehaviour
    {
        [SerializeField]
        private CharacterScriptableData[] _charactersData;

        [SerializeField]
        private CharacterUI _entitiyPrefab;

        [SerializeField]
        private Transform[] _transformsSpawnEntities = new Transform[2];

        [SerializeField]
        private EndPanel _endPanel;

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
                CharacterUI createdObject = Instantiate(_entitiyPrefab,
                    _transformsSpawnEntities[i].position,
                    Quaternion.identity,
                    _transformsSpawnEntities[i]);

                string nameEntity = _namesForEntitys[Random.Range(0, _namesForEntitys.Length)];
                CharacterScriptableData characterData = _charactersData[Random.Range(0, _charactersData.Length)];

                //EntitiesInScene[i] = createdObject.AddComponent<Character>(characterData, nameEntity, _endPanel);

                //createdObject.sprite = characterData.SpriteCharacter;

                HPBar hpBarCharacter = CreateHPBar(createdObject, EntitiesInScene[i]);

                TMP_Text[] poolTextsCharacter = CreatePoolTexts(createdObject, createdObject.CountTextsInPool);

                createdObject.InitializeVariables(hpBarCharacter, poolTextsCharacter);
            }

            EntitiesInScene[0].StartAttack(EntitiesInScene[1]);
            EntitiesInScene[1].StartAttack(EntitiesInScene[0]);
        }

        private Character[] CreateCharacter(CharacterUI chareacter, int countTextsInPool)
        {
            string nameEntity = _namesForEntitys[Random.Range(0, _namesForEntitys.Length)];
            CharacterScriptableData characterData = _charactersData[Random.Range(0, _charactersData.Length)];

            return null;
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

        private HPBar CreateHPBar(CharacterUI character, Character enemy)
        {
            HPBar hpBar = Instantiate(_prefabHPBar, character.TransformSpawnHPBar.position, Quaternion.identity, _parentSpawnObjects);
            hpBar.InitializeValues(character.TransformSpawnHPBar, enemy);

            return hpBar;
        }
    }
}