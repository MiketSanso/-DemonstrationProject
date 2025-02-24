using UnityEngine;
using UnityEngine.UI;

namespace GameScene.Level
{
    public class ReloadLevel : MonoBehaviour
    {
        [SerializeField]
        private StartGame _startGame;

        [SerializeField]
        private EndPanelSettings _endPanelSettings;

        [SerializeField]
        private Button _buttonRestart;

        public void Awake()
        {
            _buttonRestart.onClick.AddListener(ReloadingLevel);
        }

        public void ReloadingLevel()
        {
            for (int i = 0; i < _startGame.EntitiesInScene.Length; i++)
            {
                if (_startGame.EntitiesInScene[i] != null)
                {
                    _startGame.EntitiesInScene[i].DestroyThisObject();
                }
            }

            _endPanelSettings.DeactivateEndPanel();
            _startGame.CreateEntitys();
        }
    }
}