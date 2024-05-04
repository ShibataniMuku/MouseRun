using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField]
    private GameObject _blinderView;
    [SerializeField]
    private GameObject _audioManager;

    public override void InstallBindings()
    {
        Container
            .BindInterfacesAndSelfTo<SceneTransitioner>()
            .AsSingle();
        Container
            .BindInterfacesAndSelfTo<BlinderPresenter>()
            .AsSingle();
        Container
            .BindInterfacesAndSelfTo<InheritorBetweenScenes>()
            .AsSingle();

        Container
            .BindInterfacesAndSelfTo<BlinderView>()
            .FromComponentInNewPrefab(_blinderView)
            .AsSingle();

        Container
            .Bind<AudioManager>()
            .FromComponentInNewPrefab(_audioManager)
            .AsSingle();
        Container
            .BindInterfacesAndSelfTo<AudioPresenter>()
            .AsSingle();

    }
}