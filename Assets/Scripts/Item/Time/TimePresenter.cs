using Cysharp.Threading.Tasks;
using UniRx;
using Zenject;

public class TimePresenter : IInitializable
{
    private TimeManager _timeManager;
    private TimeView _timeView;

    private CompositeDisposable _compositeDisposable;

    private TimePresenter(TimeManager timeManager, TimeView timeView)
    {
        _timeManager = timeManager;
        _timeView = timeView;
    }

    public void Initialize()
    {
        _compositeDisposable = new CompositeDisposable();

        _timeManager.MainTimer.RemainingTime
            .Subscribe(x =>
            {
                _timeView.ResetTimeText(x);
            })
            .AddTo(_compositeDisposable);
    }
}
