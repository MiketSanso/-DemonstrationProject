<<<<<<< HEAD
using GameScene.Characters;
using TMPro;
=======
>>>>>>> 1ae4312d68fd11c59cbe48c26c578b07b4bdda25
using UnityEngine;
using UnityEngine.UI;

namespace GameScene.Level
{
    public class ReloadLevel : MonoBehaviour
    {
        [SerializeField]
        private CharactersFactory _charactersFactory;

        [SerializeField]
        private EndPanelSettings _endPanelSettings;

        [SerializeField]
        private Button _buttonRestart;

        private void Awake()
        {
            _buttonRestart.onClick.AddListener(ReloadingLevel);
        }

        public void ReloadingLevel()
        {
            if (_charactersFactory.EntitiesInScene != null)
            {
                for (int i = 0; i < _charactersFactory.EntitiesInScene.Length; i++)
                {
                    if (_charactersFactory.EntitiesInScene[i] != null)
                    {
<<<<<<< HEAD
                        if (_charactersFactory.EntitiesInScene[i].TryGetComponent(out CharacterUI characterUI))
                        {
                            characterUI.DestroyThisObject();
                        }
                        else
                            Debug.LogError("CharacterUI у одного из объектов отсутствует!");
=======
                        _charactersFactory.EntitiesInScene[i].DestroyThisObject();
>>>>>>> 1ae4312d68fd11c59cbe48c26c578b07b4bdda25
                    }
                }
            }

            _endPanelSettings.DeactivateEndPanel();
            _charactersFactory.CreateCharacters();
        }
    }
}