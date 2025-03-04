using GameScene.Characters;
using GameScene.Characters.Archer;
using GameScene.Characters.Mage;
using GameScene.Characters.Warrior;
using TMPro;
using UnityEngine;

namespace GameScene.Level
{
    public class CharactersFactory : MonoBehaviour
    {
        [SerializeField]
        private CharacterUI _entitiyPrefab;

        [SerializeField]
        private CharacterScriptableData[] _charactersData;

        [SerializeField]
        private Transform[] _transformsSpawn = new Transform[2];

        [SerializeField]
        private EndPanel _endPanel;

        [SerializeField]
        private Transform _parentSpawnUI;

        [SerializeField]
        private HPBar _prefabHPBar;

        [SerializeField]
        private TMP_Text _textPrefab;

        [SerializeField]
        private string[] _namesForCharacter;

        public Character[] Characters { get; private set; }

        public CharacterUI[] CharactersInScene { get; private set; } = new CharacterUI[2];

        public void CreateCharacters()
        {
            Characters = new Character[2];

            for (int i = 0; i < 2; i++)
            {
                CharactersInScene[i] = Instantiate(_entitiyPrefab,
                    _transformsSpawn[i].position,
                    Quaternion.identity,
                    _transformsSpawn[i]);

                CharacterScriptableData characterData = _charactersData[Random.Range(0, _charactersData.Length)];
                string nameEntity = _namesForCharacter[Random.Range(0, _namesForCharacter.Length)];
                Characters[i] = ConstructCharacter(characterData, nameEntity, _endPanel);

                CharactersInScene[i].GetComponent<SpriteRenderer>().color = characterData.Color;

                HPBar hpBarCharacter = CreateHPBar(CharactersInScene[i], Characters[i]);
                TMP_Text[] poolTextsCharacter = CreatePoolTexts(CharactersInScene[i], CharactersInScene[i].SizePool);
                CharactersInScene[i].InitializeVariables(hpBarCharacter, poolTextsCharacter, Characters[i]);
            }

            Characters[0].StartAttack(Characters[1]);
            Characters[1].StartAttack(Characters[0]);
        }

        private Character ConstructCharacter(CharacterScriptableData characterData, string nameCharacter, EndPanel endPanel)
        {
            if (characterData.CharacterType == CharacterType.Warrior)
            {
                return new Warrior(characterData, nameCharacter, endPanel);
            }
            else if (characterData.CharacterType == CharacterType.Mage)
            {
                return new Mage(characterData, nameCharacter, endPanel);
            }
            else if (characterData.CharacterType == CharacterType.Archer)
            {
                return new Archer(characterData, nameCharacter, endPanel);
            }
            else
            {
                Debug.LogError("ѕоле CharacterType у CharacterScriptableData либо не заполнено, либо услови€ не дополнены.");
                return null;
            }
        }

        private TMP_Text[] CreatePoolTexts(CharacterUI chareacter, int countTextsInPool)
        {
            TMP_Text[] poolTexts = new TMP_Text[countTextsInPool];

            for (int i = 0; i < countTextsInPool; i++)
            {
                poolTexts[i] = Instantiate(_textPrefab, chareacter.TransformSpawnText.position, Quaternion.identity, _parentSpawnUI);
            }

            return poolTexts;
        }

        private HPBar CreateHPBar(CharacterUI characterUI, Character character)
        {
            HPBar hpBar = Instantiate(_prefabHPBar, characterUI.TransformSpawnHPBar.position, Quaternion.identity, _parentSpawnUI);
            hpBar.InitializeValues(characterUI.TransformSpawnHPBar, character);

            return hpBar;
        }
    }
}