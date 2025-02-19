using UnityEngine;

public class AddingValueToHealthUseCase : MonoBehaviour
{
    public void Execute(ref int _healthEntity, int valueChange, int _maxHealthEntity)
    {
        _healthEntity = Mathf.Clamp(_healthEntity + valueChange, 0, _maxHealthEntity);
    }
}
