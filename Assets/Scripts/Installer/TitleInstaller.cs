using UnityEngine;
using Zenject;

public class TitleInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container
            .BindInterfacesAndSelfTo<TitlePhase>()
            .AsSingle();
    }
}