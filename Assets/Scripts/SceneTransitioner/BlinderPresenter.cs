using Cysharp.Threading.Tasks;
using System.Threading;
using Zenject;

public class BlinderPresenter : IInitializable
{
    [Inject]
    private BlinderView _blinderView;
    [Inject]
    private SceneTransitioner _sceneTransitioner;

    public BlinderPresenter(SceneTransitioner blinderModel)
    {
        _sceneTransitioner = blinderModel;
    }

    public void Initialize()
    {
        _sceneTransitioner.OnStartBlackIn += StartBlackInHandler;
        _sceneTransitioner.OnStartBlackOut += StartBlackOutHandler;
    }

    /// <summary>
    /// ブラックイン開始時に呼ばれる
    /// </summary>
    /// <returns></returns>
    private async UniTask StartBlackInHandler()
    {
        var cts = new CancellationTokenSource();
        var token = cts.Token;

        await _blinderView.CircleIn(token);
    }

    /// <summary>
    /// ブラックアウト開始時に呼ばれる
    /// </summary>
    /// <returns></returns>
    private async UniTask StartBlackOutHandler()
    {
        var cts = new CancellationTokenSource();
        var token = cts.Token;

        await _blinderView.CircleOut(token);
    }
}
