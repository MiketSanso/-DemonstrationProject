using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    private Transform _transformOverEntity;

    [SerializeField]
    private Slider _sliderHP;

    [SerializeField]
    private TMP_Text _textHP;

    public void Update()
    {
        transform.position = _transformOverEntity.position;
    }

    public void InitializeValues(ref Transform tranformEntity)
    {
        _transformOverEntity = tranformEntity;
    }

    public void ChangeHPBar(int _healthValue, int _maxHealthValue)
    {
        _textHP.text = _healthValue.ToString();
        _sliderHP.value = (float)_healthValue / (float)_maxHealthValue;
    }
}
