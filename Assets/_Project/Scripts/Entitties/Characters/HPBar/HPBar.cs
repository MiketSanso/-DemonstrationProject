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

        private AttackEnemy _attackEnemy;

        private Transform _transformOverEntity;

        private void Update()
        {
            transform.position = _transformOverEntity.position;
        }

        private void Start()
        {
            _attackEnemy.ChangeHPBar += ChangeHPBar;
        }

        private void OnDisable()
        {
            _attackEnemy.ChangeHPBar -= ChangeHPBar;
        }

        public void InitializeValues(Transform tranformEntity, AttackEnemy attackEnemy)
        {
            _transformOverEntity = tranformEntity;
            _attackEnemy = attackEnemy;
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
