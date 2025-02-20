using UnityEngine;
using System.Threading.Tasks;
using DG.Tweening;
using TMPro;

namespace Domain.Business.UseCases
{
    public class CreateTextFlyAnimationUnderEntityUseCase
    {
        public async Task Execute(TMP_Text text)
        {
            text.transform.DOMove(new Vector3(text.transform.position.x, text.transform.position.y + 3), 3);
            text.DOColor(new Vector4(text.color.r, text.color.g, text.color.b, 0), 3);
        }
    }

}