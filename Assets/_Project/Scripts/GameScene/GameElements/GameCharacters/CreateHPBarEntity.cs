using GameScene.HPBars;
using UnityEngine;
using Zenject;

public class CreateHPBarEntity : MonoBehaviour
{

    [SerializeField]
    protected Transform TransformSpawnHPBar;

    [Inject]
    protected Transform ParentHPBars;

    [SerializeField]
    private HPBar PrefabHPBar;

    public HPBar HpBar { get; private set; }

    public void CreateHPBar()
    {
        HpBar = Instantiate(PrefabHPBar, TransformSpawnHPBar.position, Quaternion.identity, ParentHPBars);

        HpBar.InitializeValues(TransformSpawnHPBar);
    }

    [Inject]
    private void Initialize()
    {
        CreateHPBar();
    }
}
