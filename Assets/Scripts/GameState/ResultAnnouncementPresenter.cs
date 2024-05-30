using Cysharp.Threading.Tasks;
using Zenject;

public class ResultAnnouncementPresenter : IInitializable
{
    private ResultPhase _resultPhase;
    private ResultAnnouncementView _resultAnnouncementView;

    private ResultAnnouncementPresenter(ResultPhase resultPhase, ResultAnnouncementView resultAnnouncementView)
    {
        _resultPhase = resultPhase;
        _resultAnnouncementView = resultAnnouncementView;
    }

    public void Initialize()
    {
        _resultPhase.OnShowResult += ShowResult;
    }

    private async UniTask ShowResult(ResultInfo resultInfo)
    {
        await _resultAnnouncementView.ShowResult(resultInfo);
    }
}
