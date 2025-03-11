using GameScene.Characters;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace GameScene.Level
{
    public class ReloaderLevel : MonoBehaviour
    {
        public event Action OnReloaded;
        
        [SerializeField] private CharactersFactory _charactersFactory;
        [SerializeField] private Button _button;
        
        private EndPanelController _endPanelController;

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
            foreach(Character character in _charactersFactory.Characters)
            {
                character?.Destroy();
            }
            
            _charactersFactory.Create();
            OnReloaded?.Invoke();
        }
    }
}