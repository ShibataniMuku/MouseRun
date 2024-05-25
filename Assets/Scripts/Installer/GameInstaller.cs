using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    //[SerializeField]
    //private string managerResourcesPath = "Manager/ItemManager";

    [SerializeField]
    private GameObject _manager;

    public override void InstallBindings()
    {
        Container
            .BindInterfacesAndSelfTo<PlayingPhase>()
            .AsSingle();

        Container
            .BindInterfacesAndSelfTo<GameStarterPresenter>()
            .AsSingle();

        Container
            .BindInterfacesAndSelfTo<ScoreItemManager>()
            .FromComponentInNewPrefab(_manager)
            .AsSingle();
        Container
            .BindInterfacesAndSelfTo<ScoreManager>()
            .AsSingle();
        Container
            .BindInterfacesAndSelfTo<ScorePresenter>()
            .AsSingle();

        //Container
        //    .BindInterfacesAndSelfTo<TimeItemManager>()
        //    .AsSingle();
        Container
            .BindInterfacesAndSelfTo<TimeManager>()
            .AsSingle();
        Container.BindInterfacesAndSelfTo<TimePresenter>()
            .AsSingle();

        Container.BindInterfacesAndSelfTo<ItemManager>()
            .AsSingle();
    }
}