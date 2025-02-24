using UnityEngine;

namespace GameScene.Level
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField]
        private CharactersFa�tory _charactersFactory;

        void Start()
        {
            _charactersFactory.CreateCharacters();
        }
    }
}