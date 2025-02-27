using Zenject;

namespace GameScene.Level
{
    public class EntryPoint : IInitializable
    {
        [Inject]
        private CharactersFactory _charactersFactory;

        public void Initialize()
        {
            _charactersFactory.CreateCharacters();
        }
    }
}