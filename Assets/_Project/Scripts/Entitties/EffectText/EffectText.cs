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

        public static implicit operator TMP_Text(EffectText d) => d._text;
    }
}