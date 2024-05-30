using UniRx;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Zenject;

public class ScorePresenter : IInitializable
{
    private ScoreManager _scoreManager;
    private ScoreView  _scoreView;

    private CompositeDisposable _compositeDisposable;

    private ScorePresenter(ScoreManager scoreManager, ScoreView scoreView)
    {
        _scoreManager = scoreManager;
        _scoreView = scoreView;
    }

    public void Initialize()
    {
        _compositeDisposable = new CompositeDisposable();

        _scoreManager.CurerntScore
            .SkipLatestValueOnSubscribe()
            .Subscribe(x =>
            {
                _scoreView.ResetScoreText(x);
            })
            .AddTo(_compositeDisposable);
    }
}
