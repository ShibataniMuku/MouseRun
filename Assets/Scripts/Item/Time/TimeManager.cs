using UniRx;

public class TimeManager
{
    public IReadOnlyReactiveProperty<float> CurerntTime => _currentTime;
    private readonly ReactiveProperty<float> _currentTime = new ReactiveProperty<float>(0);

    public void AddTime(float time)
    {
        _currentTime.Value += time;
    }
}
