using UnityEngine;

namespace GameScene.Repositories
{
    [CreateAssetMenu(fileName = "Repositories", menuName = "Names", order = 51)]
    public class NamesRepository : ScriptableObject
    {
        [SerializeField] private string[] names;

        public string GetRandomName()
        {
            return names[Random.Range(0, names.Length)];
        }
    }
}
