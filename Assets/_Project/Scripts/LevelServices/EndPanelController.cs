using GameScene.Characters;

namespace GameScene.Level
{
    public class EndPanelController
    {
        private readonly ReloaderLevel _reloader;
        private readonly EndPanel _endPanel;
        private readonly Character _firstCharacter;
        private readonly Character _secondCharacter;

        public EndPanelController(EndPanel endPanel, Character firstCharacter, Character secondCharacter, ReloaderLevel reloader)
        {
            _endPanel = endPanel;
            _firstCharacter = firstCharacter;
            _secondCharacter = secondCharacter;
            _reloader = reloader;
            
            _reloader.OnReloaded += Deactivate;
            _firstCharacter.OnWin += Activate;
            _secondCharacter.OnWin += Activate;
            _endPanel.OnDestroyed += Destroy;
        }
        
        private void Destroy()
        {
            _reloader.OnReloaded -= Deactivate;
            _firstCharacter.OnWin -= Activate;
            _secondCharacter.OnWin -= Activate;
            _endPanel.OnDestroyed -= Destroy;
        }

        private void Deactivate()
        {
            _endPanel.Deactivate();
        }
        
        private void Activate(Character character)
        {
            _endPanel.Activate(character.Name);
        }
    }
}