using UnityEngine;

namespace GameScene.Level.Texts
{
    [CreateAssetMenu(fileName = "Texts", menuName = "TextsRepository", order = 0)]
    public class TextsRepository : ScriptableObject
    {
        [field: SerializeField] public string UsePerk { get; private set; }
        [field: SerializeField] public string EndWinner { get; private set; }
        [field: SerializeField] public string OfferReplay { get; private set; }
    }
}