using UnityEngine;
using Zenject;

public class ResultInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container
            .BindInterfacesAndSelfTo<ResultPhase>()
            .AsSingle();
    }
}