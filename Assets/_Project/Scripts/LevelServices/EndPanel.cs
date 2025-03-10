using UnityEngine;
using TMPro;
using GameScene.Characters;

namespace GameScene.Level
{
    public class EndPanel : MonoBehaviour
    {
        [SerializeField] private GameObject _objectPanel;

        [SerializeField] private TMP_Text _text;

        private Character _firstCharacter;
        private Character _secondCharacter;

        private void OnDestroy()
        {
            _firstCharacter.OnWin -= Activate;
            _secondCharacter.OnWin -= Activate;
        }
        
        public void Subscribe(Character firstCharacter, Character secondCharacter)
        {
            _firstCharacter = firstCharacter;
            _secondCharacter = secondCharacter;
            
            firstCharacter.OnWin += Activate;
            secondCharacter.OnWin += Activate;
        }

        public void Activate(string nameEntity)
        {
            _objectPanel.SetActive(true);
            _text.text = $"������� ����� � ����� {nameEntity}! ������ ������� ��� ���?";
        }

        public void Deactivate()
        {
            _objectPanel.SetActive(false);
        }
    }
}