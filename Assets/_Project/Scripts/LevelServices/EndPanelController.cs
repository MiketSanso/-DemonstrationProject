using UnityEngine;
using GameScene.Characters;

namespace GameScene.Level
{
    public class EndPanelController : MonoBehaviour
    {
        [SerializeField] private EndPanel _endPanel;

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
            
            _firstCharacter.OnWin += Activate;
            _secondCharacter.OnWin += Activate;
        }

        public void Deactivate()
        {
            _endPanel.Deactivate();
        }
        
        private void Activate(string nameCharacter)
        {
            _endPanel.Activate(nameCharacter);
        }
    }
}