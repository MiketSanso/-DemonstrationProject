using GameScene.HPBars;
using UnityEngine;
using Zenject;

public class CreateHPBarEntity : MonoBehaviour
{
    public HPBar HpBar { get; private set; }

    [SerializeField]
    private HPBar PrefabHPBar;

    [SerializeField]
    protected Transform TransformSpawnHPBar;

    [Inject]
    protected Transform ParentHPBars;

    [Inject]
    private void Initialize()
    {
        CreateHPBar();
    }

    public void CreateHPBar()
    {
        HpBar = Instantiate(PrefabHPBar, TransformSpawnHPBar.position, Quaternion.identity, ParentHPBars);

        HpBar.InitializeValues(TransformSpawnHPBar);
    }
}
