using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameScene.Characters
{
    public class HPBar : MonoBehaviour
    {
        [SerializeField] private Slider _sliderHP;

        [SerializeField] private TMP_Text _textHP;

        private Character _character;

        private Transform _transformOverEntity;

        private void Update()
        {
            transform.position = _transformOverEntity.position;
        }

        private void OnDisable()
        {
            _character.HealthCharacter.OnChanged -= Change;
        }

        public void InitializeValues(Transform tranformEntity, Character character)
        {
            _transformOverEntity = tranformEntity;
            _character = character;

            _character.HealthCharacter.OnChanged += Change;
        }

        public void Change(int _healthValue)
        {
            _textHP.text = _healthValue.ToString() + " HP";
            _sliderHP.value = (float)_healthValue / (float)_character.MaxHealthEntity;
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
