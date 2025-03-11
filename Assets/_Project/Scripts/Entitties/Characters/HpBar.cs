using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameScene.Characters
{
    public class HpBar : MonoBehaviour
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
            _character.Health.OnChanged -= Change;
        }

        public void Initialize(Transform transformEntity, Character character)
        {
            _transformOverEntity = transformEntity;
            _character = character;

            _character.Health.OnChanged += Change;
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
        
        private void Change(int healthValue)
        {
            _textHP.text = healthValue.ToString() + " HP";
            _sliderHP.value = (float)healthValue / _character.MaxHealth;
        }
    }
}
