using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameScene.Characters
{
    public class HPBar : MonoBehaviour
    {
        [SerializeField]
        private Slider _sliderHP;

        [SerializeField]
        private TMP_Text _textHP;

        private Character _character;

        private Transform _transformOverEntity;

        private void Update()
        {
            transform.position = _transformOverEntity.position;
        }

        private void OnDisable()
        {
            _character.HealthChanged -= ChangeHPBar;
        }

        public void InitializeValues(Transform tranformEntity, Character character)
        {
            _transformOverEntity = tranformEntity;
            _character = character;

            EventSubscription();
        }

        public void ChangeHPBar(int _healthValue, int _maxHealthValue)
        {
            _textHP.text = _healthValue.ToString() + " HP";
            _sliderHP.value = (float)_healthValue / (float)_maxHealthValue;
        }

        public void DestroyHPBar()
        {
            Destroy(gameObject);
        }

        private void EventSubscription()
        {
            _character.HealthChanged += ChangeHPBar;
        }
    }
}
