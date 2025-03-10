using GameScene.Characters;
using GameScene.Characters.Archer;
using GameScene.Characters.Mage;
using GameScene.Characters.Warrior;
using GameScene.Repositories;
using TMPro;
using UnityEngine;

namespace GameScene.Level
{
    public class CharactersFactory : MonoBehaviour
    {
        [SerializeField] private CharacterUI _characterPrefab;

        [SerializeField] private CharacterConfig[] _charactersData;

        [SerializeField] private Transform[] _transformsSpawn = new Transform[2];

        [SerializeField] private EndPanel _endPanel;

        [SerializeField] private Transform _parentSpawnUI;

        [SerializeField] private HPBar _prefabHPBar;

        [SerializeField] private TMP_Text _textPrefab;

        [SerializeField] private NamesRepository _namesRepository;

        public Character[] Characters { get; private set; }

        public CharacterUI[] CharactersUI { get; private set; } = new CharacterUI[2];

        public void CreateCharacters()
        {
            Characters = new Character[2];

            for (int i = 0; i < 2; i++)
            {
                CharactersUI[i] = Instantiate(_characterPrefab,
                    _transformsSpawn[i].position,
                    Quaternion.identity,
                    _transformsSpawn[i]);

                CharacterConfig characterConfig = _charactersData[Random.Range(0, _charactersData.Length)];
                Characters[i] = ConstructCharacter(characterConfig, _namesRepository);

                CharactersUI[i].GetComponent<SpriteRenderer>().color = characterConfig.Color;

                HPBar hpBarCharacter = CreateHpBar(CharactersUI[i], Characters[i]);
                TMP_Text[] poolTextsCharacter = CreatePoolTexts(CharactersUI[i], CharactersUI[i].SizePool);
                CharactersUI[i].Initialize(hpBarCharacter, poolTextsCharacter, Characters[i]);
            }

            Characters[0].StartAttack(Characters[1]);
            Characters[1].StartAttack(Characters[0]);
        }

        private Character ConstructCharacter(CharacterConfig characterConfig, NamesRepository namesRepository)
        {
            if (characterConfig.CharacterType == CharacterType.Warrior)
            {
                return new Warrior(characterConfig, namesRepository);
            }
            else if (characterConfig.CharacterType == CharacterType.Mage)
            {
                return new Mage(characterConfig, namesRepository);
            }
            else if (characterConfig.CharacterType == CharacterType.Archer)
            {
                return new Archer(characterConfig, namesRepository);
            }
            else
            {
                Debug.LogError("���� CharacterType � CharacterScriptableData ���� �� ���������, ���� ������� �� ���������.");
                return null;
            }
        }

        private TMP_Text[] CreatePoolTexts(CharacterUI character, int countTextsInPool)
        {
            TMP_Text[] poolTexts = new TMP_Text[countTextsInPool];

            for (int i = 0; i < countTextsInPool; i++)
            {
                poolTexts[i] = Instantiate(_textPrefab, character.TransformSpawnText.position, Quaternion.identity, _parentSpawnUI);
            }

            return poolTexts;
        }

        private HPBar CreateHpBar(CharacterUI characterUI, Character character)
        {
            HPBar hpBar = Instantiate(_prefabHPBar, characterUI.TransformSpawnHPBar.position, Quaternion.identity, _parentSpawnUI);
            hpBar.InitializeValues(characterUI.TransformSpawnHPBar, character);

            return hpBar;
        }
    }
}