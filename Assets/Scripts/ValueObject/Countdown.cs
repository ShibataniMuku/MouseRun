using Cysharp.Threading.Tasks;
using System;
using UniRx;
using UnityEngine;

public class Countdown
{
    public IReadOnlyReactiveProperty<float> RemainingTime => _remainingTime;
    private readonly FloatReactiveProperty _remainingTime = new FloatReactiveProperty(0);

    private bool _isCountdown = false;

    public bool IsCompleteCountdown => _isCompleteCountdown;
    private bool _isCompleteCountdown = false;

    public Countdown(TimeLimit timeLimit)
    {
        _remainingTime.Value = timeLimit.Value;
    }

    public void SetTimeLimit(TimeLimit timeLimit)
    {
        _remainingTime.Value = timeLimit.Value;
    }

    public async void StartCountdown()
    {
        _isCountdown = true;

        while (true)
        {
            if (_isCountdown)
            {
                _remainingTime.Value -= Time.deltaTime;

                if (_remainingTime.Value < 0)
                {
                    _remainingTime.Value = 0;
                    _isCompleteCountdown = true;
                    _isCountdown = false;
                }
            }

            await UniTask.Yield();
        }
    }

    public void AddTime(TimeLimit timeLimit)
    {
        _remainingTime.Value += timeLimit.Value;
    }
}
