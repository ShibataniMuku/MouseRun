using Cysharp.Threading.Tasks;
using System;
using Zenject;

public class TitlePhase : IPhase, IInitializable
{
    private SceneTransitioner _sceneTransitioner;

    private TitlePhase(SceneTransitioner sceneTransitioner)
    {
        _sceneTransitioner = sceneTransitioner;
    }

    public async void Initialize()
    {
        await OnCompleteTransition();
    }

    public async UniTask OnCompleteTransition()
    {
        await _sceneTransitioner.CompleteTransitionSceneAndBlackIn();
    }

    public UniTask OnStartTransition()
    {
        throw new NotImplementedException();
    }
}
