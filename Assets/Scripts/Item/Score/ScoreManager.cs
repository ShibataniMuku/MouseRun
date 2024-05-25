using UniRx;

public class ScoreManager
{
    public IReadOnlyReactiveProperty<Score> CurerntScore => _currentScore;
    private readonly ReactiveProperty<Score> _currentScore = new ReactiveProperty<Score>(new Score(0));

    public void AddScore(Score score)
    {
        _currentScore.Value = Score.Sum(_currentScore.Value, score);
    }
}
