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

        public void SubscribeInCharacters()
        {
            
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