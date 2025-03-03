using UnityEngine;
using TMPro;

namespace GameScene.Level
{
    public class EndPanel : MonoBehaviour
    {
        [SerializeField]
        private GameObject _objectPanel;

        [SerializeField]
        private TMP_Text _text;

        public void Activate(string nameEntity)
        {
            _objectPanel.SetActive(true);
            _text.text = $"Победил игрок с ником {nameEntity}! Хотите сыграть ещё раз?";
        }

        public void Deactivate()
        {
            _objectPanel.SetActive(false);
        }
    }
}