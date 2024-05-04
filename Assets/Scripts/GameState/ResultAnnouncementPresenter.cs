
using Unity.VisualScripting;

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
        throw new System.NotImplementedException();
    }
}
