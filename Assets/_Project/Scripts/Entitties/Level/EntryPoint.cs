<<<<<<< HEAD
using Zenject;

namespace GameScene.Level
{
    public class EntryPoint : IInitializable
    {
        [Inject]
        private CharactersFactory _charactersFactory;

        public void Initialize()
=======
using UnityEngine;

namespace GameScene.Level
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField]
        private CharactersFactory _charactersFactory;

        void Start()
>>>>>>> 1ae4312d68fd11c59cbe48c26c578b07b4bdda25
        {
            _charactersFactory.CreateCharacters();
        }
    }
}