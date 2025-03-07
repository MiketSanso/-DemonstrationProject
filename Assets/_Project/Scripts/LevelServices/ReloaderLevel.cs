using GameScene.Characters;
using UnityEngine;
using UnityEngine.UI;

namespace GameScene.Level
{
    public class ReloaderLevel : MonoBehaviour
    {
        [SerializeField]
        private CharactersFactory _charactersFactory;

        [SerializeField]
        private EndPanel _endPanel;

        [SerializeField]
        private Button _button;

        private void Awake()
        {
            _button.onClick.AddListener(Reload);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(Reload);
        }

        private void Reload()
        {
            if (_charactersFactory.CharactersUI != null)
            {
                foreach(Character character in _charactersFactory.Characters)
                {
                    character.StartDestroy();
                }
            }

            _endPanel.Deactivate();
            _charactersFactory.CreateCharacters();
        }
    }
}