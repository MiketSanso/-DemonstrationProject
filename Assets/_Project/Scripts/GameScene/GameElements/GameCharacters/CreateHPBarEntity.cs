using GameScene.HPBars;
using UnityEngine;
using Zenject;

namespace GameScene.Character
{
    public class CreateHPBarEntity : MonoBehaviour
    {
        public Transform ParentHPBars;

        [SerializeField]
        private Transform TransformSpawnHPBar;

        [SerializeField]
        private HPBar PrefabHPBar;

        public HPBar HpBar { get; private set; }

        public void CreateHPBar()
        {
            HpBar = Instantiate(PrefabHPBar, TransformSpawnHPBar.position, Quaternion.identity, ParentHPBars);

            HpBar.InitializeValues(TransformSpawnHPBar);
        }

        public void DestroyHPBar()
        {
            Destroy(HpBar);
        }
    }
}
