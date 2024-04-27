using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    //[SerializeField]
    //private string managerResourcesPath = "Manager/ItemManager";

    [SerializeField]
    private GameObject _itemManager;

    public override void InstallBindings()
    {
        Container
            .Bind(typeof(ScoreItemManager), typeof(IInitializable))
            .To<ScoreItemManager>()
            .FromComponentInNewPrefab(_itemManager)
            .AsSingle();
        //Container
        //    .BindInterfacesAndSelfTo<TimeItemManager>()
        //    .AsSingle();
        Container
            .BindInterfacesAndSelfTo<ScoreManager>()
            .AsSingle();
        Container
            .BindInterfacesAndSelfTo<TimeManager>()
            .AsSingle();
        Container
            .Bind<ItemManager>()
            .To<ItemManager>()
            .AsSingle();
    }
}