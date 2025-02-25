using UnityEngine;

namespace GameScene.Level
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField]
        private CharactersFactory _charactersFactory;

        void Start()
        {
            _charactersFactory.CreateCharacters();
        }
    }
}