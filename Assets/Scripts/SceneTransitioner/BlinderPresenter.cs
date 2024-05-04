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
    /// �u���b�N�C���J�n���ɌĂ΂��
    /// </summary>
    /// <returns></returns>
    private async UniTask StartBlackInHandler()
    {
        var cts = new CancellationTokenSource();
        var token = cts.Token;

        await _blinderView.CircleIn(token);
    }

    /// <summary>
    /// �u���b�N�A�E�g�J�n���ɌĂ΂��
    /// </summary>
    /// <returns></returns>
    private async UniTask StartBlackOutHandler()
    {
        var cts = new CancellationTokenSource();
        var token = cts.Token;

        await _blinderView.CircleOut(token);
    }
}
