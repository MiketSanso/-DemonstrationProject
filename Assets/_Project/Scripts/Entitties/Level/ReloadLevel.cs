using UnityEngine;
using UnityEngine.UI;

namespace GameScene.Level
{
    public class ReloadLevel : MonoBehaviour
    {
        [SerializeField]
        private CharactersFactory _charactersFactory;

        [SerializeField]
        private EndPanel _endPanelSettings;

        [SerializeField]
        private Button _buttonRestart;

        private void Awake()
        {
            _buttonRestart.onClick.AddListener(ReloadingLevel);
        }

        public void ReloadingLevel()
        {
            if (_charactersFactory.CharactersInScene != null)
            {
                for (int i = 0; i < _charactersFactory.CharactersInScene.Length; i++)
                {
                    if (_charactersFactory.CharactersInScene[i] != null)
                    {
                        _charactersFactory.Characters[i].StartDestroy();
                    }
                }
            }

            _endPanelSettings.Deactivate();
            _charactersFactory.CreateCharacters();
        }
    }
}