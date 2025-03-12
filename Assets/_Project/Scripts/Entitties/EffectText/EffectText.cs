using UnityEngine;
using TMPro;

namespace GameScene.Level.Texts
{
    public class EffectText : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _text;

        public void Destroy()
        {
            Destroy(_text);
        }

        public EffectText Create(Transform transformSpawn, Transform parentSpawn)
        {
            return Instantiate(this, transformSpawn.position, Quaternion.identity, parentSpawn);
        }

        public static implicit operator TMP_Text(EffectText d) => d._text;
    }
}