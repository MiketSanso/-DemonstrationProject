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

        [SerializeField] private EndPanelController _endPanelController;

        [SerializeField] private Transform _parentSpawnUI;

        [SerializeField] private HpBar _prefabHPBar;

        [SerializeField] private TMP_Text _textPrefab;

        [SerializeField] private NamesRepository _namesRepository;
        
        private readonly CharacterUI[] _charactersUI = new CharacterUI[2];

        public Character[] Characters { get; private set; } = new Character[2];
        
        public void CreateCharacters()
        {
            Characters = new Character[2];

            for (int i = 0; i < 2; i++)
            {
                _charactersUI[i] = Instantiate(_characterPrefab,
                    _transformsSpawn[i].position,
                    Quaternion.identity,
                    _transformsSpawn[i]);

                CharacterConfig characterConfig = _charactersData[Random.Range(0, _charactersData.Length)];
                Characters[i] = ConstructCharacter(characterConfig, _namesRepository);

                _charactersUI[i].GetComponent<SpriteRenderer>().color = characterConfig.Color;

                HpBar hpBarCharacter = CreateHpBar(_charactersUI[i], Characters[i]);
                TMP_Text[] poolTextsCharacter = CreatePoolTexts(_charactersUI[i], _charactersUI[i].SizePool);
                _charactersUI[i].Initialize(hpBarCharacter, poolTextsCharacter, Characters[i]);
            }

            Characters[0].StartAttack(Characters[1]);
            Characters[1].StartAttack(Characters[0]);
            
            _endPanelController.Subscribe(Characters[0], Characters[1]);
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
                Debug.LogError("Новый CharacterType в CharacterScriptableData был добавлен не верно!");
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

        private HpBar CreateHpBar(CharacterUI characterUI, Character character)
        {
            HpBar hpBar = Instantiate(_prefabHPBar, characterUI.TransformSpawnHpBar.position, Quaternion.identity, _parentSpawnUI);
            hpBar.InitializeValues(characterUI.TransformSpawnHpBar, character);

            return hpBar;
        }
    }
}