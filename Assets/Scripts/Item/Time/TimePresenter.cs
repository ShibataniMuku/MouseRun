using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using Zenject;

public class TimePresenter : IInitializable
{
    [SerializeField]
    TimeView _timeView;

    private CompositeDisposable _compositeDisposable;
    
    public void Initialize()
    {
        _compositeDisposable = new CompositeDisposable();

        PlayingPhase.playingPhaseInstance.Countdown.RemainingTime
            .Subscribe(x =>
            {
                _timeView.ResetTimeText(x);
            })
            .AddTo(_compositeDisposable);
    }
}
