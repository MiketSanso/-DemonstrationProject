using UnityEngine;

namespace GameScene.Level.Texts
{
    public class PoolEffectTexts
    {
        public readonly EffectText[] Texts;

        public PoolEffectTexts(EffectText prefab, int countTexts, Transform transformSpawn, Transform transformParent)
        {
            Texts = new EffectText[countTexts];
            for (int i = 0; i < countTexts; i++)
            {
                Texts[i] = prefab.Create(transformSpawn, transformParent);
            }
        }

        public void DestroyPool()
        {
            foreach (EffectText text in Texts)
            {
                text.Destroy();
            }
        }
    }
}