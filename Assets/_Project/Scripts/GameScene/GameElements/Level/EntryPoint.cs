using UnityEngine;

namespace GameScene.Level
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField]
        private CharactersFañtory _charactersFactory;

        void Start()
        {
            _charactersFactory.CreateCharacters();
        }
    }
}