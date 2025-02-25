using UnityEngine;
using TMPro;

namespace GameScene.Level
{
    public class EndPanelSettings : MonoBehaviour
    {
        [SerializeField]
        private GameObject _endPanel;

        [SerializeField]
        private TMP_Text _textInEndPanel;

        public void ActivateEndPanel(string nameEntity)
        {
            _endPanel.SetActive(true);
            _textInEndPanel.text = $"������� ����� � ����� {nameEntity}! ������ ������� ��� ���?";
        }

        public void DeactivateEndPanel()
        {
            _endPanel.SetActive(false);
        }
    }
}