using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameScene.HPBars
{
    public class HPBar : MonoBehaviour
    {
        [SerializeField]
        private Slider _sliderHP;

        [SerializeField]
        private TMP_Text _textHP;

        private Transform _transformOverEntity;

        private void Update()
        {
            transform.position = _transformOverEntity.position;
        }

        public void InitializeValues(Transform tranformEntity)
        {
            _transformOverEntity = tranformEntity;
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
    }
}
