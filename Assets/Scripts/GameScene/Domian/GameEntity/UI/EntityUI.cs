using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EntityUI : MonoBehaviour
{
    private Entity _entity;

    private HPBar _hpBar;

    [SerializeField]
    private GameObject _prefabHPBar;

    [SerializeField]
    private Transform _transformSpawnHPBar;

    [SerializeField]
    private Transform _parentHPBars;

    private List<EntityUI> _entitiesInAttaksZone = new List<EntityUI>();

    [SerializeField]
    private EntityType _entityType;

    private void Start()
    {
        CreateHPBar();

        _entity = new Entity(100, 100, 2, 5, _entityType); 
    }

    private void CreateHPBar()
    {
        GameObject _HPBarObject = Instantiate(_prefabHPBar.gameObject, _transformSpawnHPBar.position, Quaternion.identity);
        _HPBarObject.transform.SetParent(_parentHPBars);
        _HPBarObject.transform.localScale = Vector3.one;

        if (!_HPBarObject.GetComponent<HPBar>())
        {
            Debug.LogError("У префаба HPBar отсутствует скрипт HPBar");
            return;
        }

        _hpBar = _HPBarObject.GetComponent<HPBar>();

        _hpBar.InitializeValues(ref _transformSpawnHPBar);
    }

    public void AddingValueToHealth(int value)
    {
        _entity.AddingValueToHealth(value);
        _hpBar.ChangeHPBar(_entity.HealthEntity, _entity.MaxHealthEntity);
    }

    public async void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<EntityUI>() && !other.isTrigger)
        {
            _entitiesInAttaksZone.Add(other.GetComponent<EntityUI>());

            await TakeDamage();
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<EntityUI>() && !other.isTrigger)
        {
            _entitiesInAttaksZone.Remove(other.GetComponent<EntityUI>());
        }
    }

    private async Task TakeDamage()
    {
        do
        {
            int indexSelectedEntity = Random.Range(0, _entitiesInAttaksZone.Count);

            int damage = Random.Range(-_entity.BaseDamage - 3, -_entity.BaseDamage + 3);

            _entitiesInAttaksZone[indexSelectedEntity].AddingValueToHealth(damage);

            await Task.Delay(_entity.Cooldown * 1000);
        } while (_entitiesInAttaksZone.Count != 0);
    }
}
