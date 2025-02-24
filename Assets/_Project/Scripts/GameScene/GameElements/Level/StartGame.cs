using GameScene.Character;
using UnityEngine;

namespace GameScene.Level
{
    public class StartGame : MonoBehaviour
    {
        [SerializeField]
        private CharacterUI[] _entitiyPrefabs;

        [SerializeField]
        private Transform[] _transformsSpawnEntities = new Transform[2];

        public CharacterUI[] EntitiesInScene { get; private set; } = new CharacterUI[2];

        [SerializeField]
        private EndPanelSettings _endPanelSettings;

        private void Start()
        {
            CreateEntitys();
        }

        public void CreateEntitys()
        {
            for (int i = 0; i < 2; i++)
            {
                CharacterUI entityForSpawn = _entitiyPrefabs[Random.Range(0, _entitiyPrefabs.Length)];

                CharacterUI createdObject = Instantiate(entityForSpawn,
                    _transformsSpawnEntities[i].position,
                    Quaternion.identity,
                    _transformsSpawnEntities[i]);
                EntitiesInScene[i] = createdObject;

                EntitiesInScene[i].TakeDamageEnemy.EndPanelSettings = _endPanelSettings;
            }

            EntitiesInScene[0].TakeDamageEnemy.StartTaskAttack(EntitiesInScene[1], EntitiesInScene[0]);
            EntitiesInScene[1].TakeDamageEnemy.StartTaskAttack(EntitiesInScene[0], EntitiesInScene[1]);
        }
    }
}