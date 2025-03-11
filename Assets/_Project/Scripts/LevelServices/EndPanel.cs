using UnityEngine;
using System;
using GameScene.Level.Texts;
using TMPro;
using Zenject;

namespace GameScene.Level
{
    public class EndPanel : MonoBehaviour
    {
        public event Action OnDestroyed;

        [SerializeField] private GameObject _objectPanel;
        [SerializeField] private TMP_Text _text;
        
        [Inject] private readonly TextsRepository _textsRepository;

        private void OnDestroy()
        {
            OnDestroyed?.Invoke();
        }
        
        public void Activate(string nameCharacter)
        {
            _objectPanel.SetActive(true);
            _text.text = $"{_textsRepository.EndWinner} {nameCharacter}! {_textsRepository.OfferReplay}";
        }

        public void Deactivate()
        {
            _objectPanel.SetActive(false);
        }
    }
}