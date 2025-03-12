using GameScene.Characters;
using GameScene.Characters.Archer;
using GameScene.Characters.Mage;
using GameScene.Characters.Warrior;
using GameScene.Repositories;
using UnityEngine;
using GameScene.Level.Texts;
using Zenject;

namespace GameScene.Level
{
    public class CharactersFactory : MonoBehaviour
    {
        [SerializeField] private CharacterUI _characterPrefab;
        [SerializeField] private CharacterConfig[] _charactersData;
        [SerializeField] private Transform[] _transformsSpawn = new Transform[2];
        [SerializeField] private EndPanel _endPanel;
        [SerializeField] private Transform _parentSpawnUI;
        [SerializeField] private HpBar _prefabHpBar;
        [SerializeField] private EffectText textsPrefab;
        [SerializeField] private NamesRepository _namesRepository;
        [SerializeField] private ReloaderLevel _reloader;
        
        private EndPanelController _endPanelController;
        private readonly CharacterUI[] _charactersUI = new CharacterUI[2];
        
        [Inject] private TextsRepository _textsRepository;
        
        public Character[] Characters { get; private set; } = new Character[2];
        
        public void Create()
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
                
                PoolEffectTexts poolTexts = new PoolEffectTexts(textsPrefab, 
                    _charactersUI[i].SizePool, 
                    _charactersUI[i].TransformSpawnText, 
                    _parentSpawnUI);
                
                _charactersUI[i].Initialize(hpBarCharacter, poolTexts, Characters[i]);
            }

            Characters[0].StartAttack(Characters[1]);
            Characters[1].StartAttack(Characters[0]);
            
            _endPanelController = new EndPanelController(_endPanel, Characters[0], Characters[1], _reloader);
        }

        private Character ConstructCharacter(CharacterConfig characterConfig, NamesRepository namesRepository)
        {
            if (characterConfig.CharacterType == CharacterType.Warrior)
            {
                return new Warrior(characterConfig, namesRepository, _textsRepository);
            }
            if (characterConfig.CharacterType == CharacterType.Mage)
            {
                return new Mage(characterConfig, namesRepository, _textsRepository);
            }
            if (characterConfig.CharacterType == CharacterType.Archer)
            {
                return new Archer(characterConfig, namesRepository, _textsRepository);
            }

            Debug.LogError("Ни один из существующих CharacterType не подошёл");
            return null;
        }

        private HpBar CreateHpBar(CharacterUI characterUI, Character character)
        {
            HpBar hpBar = Instantiate(_prefabHpBar, characterUI.TransformSpawnHpBar.position, Quaternion.identity, _parentSpawnUI);
            hpBar.Initialize(characterUI.TransformSpawnHpBar, character);

            return hpBar;
        }
    }
}