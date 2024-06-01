using Zenject;

public class ResultInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container
            .BindInterfacesAndSelfTo<ResultPhase>()
            .AsSingle();
        Container
            .BindInterfacesAndSelfTo<ResultAnnouncementPresenter>()
            .AsSingle();
    }
}