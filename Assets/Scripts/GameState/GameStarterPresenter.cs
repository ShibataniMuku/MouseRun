using Cysharp.Threading.Tasks;
using System.Threading;
using Zenject;

public class GameStarterPresenter: IInitializable
{
    private PlayingPhase _playingPhase;
    private GameStarterView _gameStarterView;
    private SceneTransitioner _sceneTransitioner;

    GameStarterPresenter(PlayingPhase playingPhase, GameStarterView gameStarterView, SceneTransitioner sceneTransitioner)
    {
        _playingPhase = playingPhase;
        _gameStarterView = gameStarterView;
        _sceneTransitioner = sceneTransitioner;
    }

    public void Initialize()
    {
        _sceneTransitioner.OnCompleteBlackIn += StartGameAnimationHandler;
        _playingPhase.OnFinishGame += FinishGameAnimationHandler;
    }

    private async UniTask StartGameAnimationHandler()
    {
        var cts = new CancellationTokenSource();
        var token = cts.Token;

        await _gameStarterView.StartGameAnimation(token);
    }

    private async UniTask FinishGameAnimationHandler()
    {
        var cts = new CancellationTokenSource();
        var token = cts.Token;

        await _gameStarterView.FinishGameAnimation(token);
    }
}
