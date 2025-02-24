using UnityEngine;
using UnityEngine.UI;

namespace GameScene.Level
{
    public class ReloadLevel : MonoBehaviour
    {
        [SerializeField]
        private CharactersFañtory _charactersFactory;

        [SerializeField]
        private EndPanelSettings _endPanelSettings;

        [SerializeField]
        private Button _buttonRestart;

        private void Awake()
        {
            _buttonRestart.onClick.AddListener(ReloadingLevel);
        }

        public void ReloadingLevel()
        {
            for (int i = 0; i < _charactersFactory.EntitiesInScene.Length; i++)
            {
                if (_charactersFactory.EntitiesInScene[i] != null)
                {
                    _charactersFactory.EntitiesInScene[i].DestroyThisObject();
                }
            }

            _endPanelSettings.DeactivateEndPanel();
            _charactersFactory.CreateCharacters();
        }
    }
}