using System.Collections.Generic;
using Domain.Business.UseCases;
using GameScene.HPBars;
using System.Threading;
using System.Threading.Tasks;
using GameScene.GameManager;
using UnityEngine;
using Zenject;
using TMPro;

namespace GameScene.Entity
{
    public abstract class EntityUI : MonoBehaviour
    {
        public Entity _entity;

        protected HPBar _hpBar;

        protected List<EntityUI> _entitiesInAttaksZone = new List<EntityUI>();

        protected bool _perkIsActive = false;

        private CancellationTokenSource _cts;

        [SerializeField]
        protected GameObject _prefabHPBar;

        [SerializeField]
        protected Transform _transformSpawnHPBar;

        [SerializeField]
        protected Transform _transformSpawnText;

        [SerializeField]
        protected EntityType _entityType;

        [SerializeField]
        private TMP_Text _textPrefab;

        [SerializeField]
        private string[] _namesForEntitys;


        [Inject]
        private GameManagement _gameManager;

        [Inject]
        protected Transform _parentHPBars;

        [Inject]
        private CreateTextFlyAnimationUnderEntityUseCase _createTextFlyAnimationUnderEntityUseCase;

        [Inject]
        private void Initialize()
        {
            CreateHPBar();
        }

        private void Awake()
        {
            _entity = new Entity(100, 100, 2, 5, 5, 70, "ПЕРК", _namesForEntitys[Random.Range(0, _namesForEntitys.Length)],  _entityType);
        }

        protected void CreateHPBar()
        {
            GameObject _HPBarObject = Instantiate(_prefabHPBar.gameObject, _transformSpawnHPBar.position, Quaternion.identity, _parentHPBars);

            _hpBar = _HPBarObject.GetComponent<HPBar>();

            _hpBar.InitializeValues(ref _transformSpawnHPBar);
        }

        public void AddingValueToHealth(int value)
        {
            _entity.AddingValueToHealth(value);
            _hpBar.ChangeHPBar(_entity.HealthEntity, _entity.MaxHealthEntity);
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<EntityUI>() && !other.isTrigger)
            {
                _entitiesInAttaksZone.Add(other.GetComponent<EntityUI>());

                if (Random.Range(0, 101) < _entity.PercentagesChancePerk)
                {
                    StartTaskAttack();
                }
            }
        }

        public void OnTriggerExit2D(Collider2D other)
        {
            if (other.GetComponent<EntityUI>() && !other.isTrigger)
            {
                _entitiesInAttaksZone.Remove(other.GetComponent<EntityUI>());
            }
        }

        protected virtual async Task TakeDamage()
        {
            do
            {
                if (_cts == null || _cts.IsCancellationRequested || _entity.BaseDamage == 0)
                {
                    break;
                }

                int indexSelectedEntity = Random.Range(0, _entitiesInAttaksZone.Count);

                int damage = Random.Range(-_entity.BaseDamage - 3, -_entity.BaseDamage + 3) + _entity.CoefChangeDamage;

                if (damage > 0)
                    damage = 0;

                _entitiesInAttaksZone[indexSelectedEntity].AddingValueToHealth(damage);

                TMP_Text text = Instantiate(_textPrefab, _entitiesInAttaksZone[indexSelectedEntity]._transformSpawnText.position, Quaternion.identity, _parentHPBars);
                text.text = $"{damage} HP";
                await _createTextFlyAnimationUnderEntityUseCase.Execute(text);

                if (_entitiesInAttaksZone[indexSelectedEntity]._entity.HealthEntity == 0)
                {
                    _entitiesInAttaksZone[indexSelectedEntity].DestroyHPBar();
                    Destroy(_entitiesInAttaksZone[indexSelectedEntity].gameObject);
                    _gameManager.ActivateEndPanel(_entity.TextNameEntity);

                    return;
                }

                await Task.Delay(_entity.Cooldown * 1000);

                if (!_perkIsActive)
                {
                    UseEntityPerk(_entitiesInAttaksZone[indexSelectedEntity]);
                    _perkIsActive = true;
                }

                await Task.Delay(500);
            } while (_entitiesInAttaksZone.Count != 0);
        }

        public async void StartTaskAttack()
        {
            if (_entitiesInAttaksZone.Count != 0)
            {
                if (_cts == null || _cts.IsCancellationRequested)
                {
                    _cts = new CancellationTokenSource();
                }

                await TakeDamage();
            }
        }

        public void StopTaskAttack()
        {
            if (_cts != null)
            {
                _cts.Cancel();
                _cts.Dispose();
                _cts = null;
            }
        }

        protected virtual async void UseEntityPerk(EntityUI enemy)
        {
            TMP_Text text = Instantiate(_textPrefab, enemy._transformSpawnText.position, Quaternion.identity, _parentHPBars);
            text.text = $"Персонаж использовал перк: {_entity.TextApplicationsPerk}";
            await _createTextFlyAnimationUnderEntityUseCase.Execute(text);
        }

        public void DestroyHPBar()
        {
            Destroy(_hpBar.gameObject);
        }
    }
}