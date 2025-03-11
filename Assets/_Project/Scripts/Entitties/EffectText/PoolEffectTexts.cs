using TMPro;
using UnityEngine;
using DG.Tweening;

namespace GameScene.Level.Texts
{
    public class PoolEffectTexts
    {
        private readonly EffectText[] _texts;
        private readonly TextsRepository _textsRepository;

        public PoolEffectTexts(EffectText[] texts, TextsRepository textsRepository)
        {
            _texts = texts;
            _textsRepository = textsRepository;
        }
        
        public void RespawnText(TypesText typeText, string textForSpawn, Transform transformSpawnText, float heightFlyText, float speedFlyText)
        {
            foreach (TMP_Text text in _texts)
            {
                if (text.color.a == 0)
                {
                    text.color = new Vector4(text.color.r, text.color.g, text.color.b, 100);
                    text.transform.position = transformSpawnText.position;

                    if (typeText == TypesText.Damage)
                    {
                        text.text = $"-{textForSpawn}";
                    }
                    else if (typeText == TypesText.Perk)
                    {
                        text.text = $"{_textsRepository.UsePerk} {textForSpawn}";
                    }

                    text.transform.DOMove(new Vector3(text.transform.position.x, text.transform.position.y + heightFlyText), speedFlyText);
                    text.DOColor(new Vector4(text.color.r, text.color.g, text.color.b, 0), speedFlyText);
                }
            }
        }

        public void DestroyPool()
        {
            foreach (EffectText text in _texts)
            {
                text.Destroy();
            }
        }
    }
}