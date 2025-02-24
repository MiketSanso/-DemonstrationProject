using UnityEngine;
using UnityEngine.UI;

namespace GameScene.Level
{
    public class ReloadLevel : MonoBehaviour
    {
        [SerializeField]
        private CharactersFañtory _charactersFabric;

        [SerializeField]
        private EndPanelSettings _endPanelSettings;

        [SerializeField]
        private Button _buttonRestart;

        private void Awake()
        {
            _buttonRestart.onClick.AddListener(ReloadingLevel);
        }

        private void Start()
        {
            ReloadingLevel();
        }

        public void ReloadingLevel()
        {
            for (int i = 0; i < _charactersFabric.EntitiesInScene.Length; i++)
            {
                if (_charactersFabric.EntitiesInScene[i] != null)
                {
                    _charactersFabric.EntitiesInScene[i].DestroyThisObject();
                }
            }

            _endPanelSettings.DeactivateEndPanel();
            _charactersFabric.CreateCharacters();
        }
    }
}