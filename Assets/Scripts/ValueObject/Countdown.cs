using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UniRx;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    public IReadOnlyReactiveProperty<float> RemainingTime => _remainingTime;
    private readonly FloatReactiveProperty _remainingTime = new FloatReactiveProperty(0);

    private bool _isCountdown = false;

    public bool IsCompleteCountdown => _isCountdown;
    private bool _isCompleteCountdown = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_isCountdown)
        {
            _remainingTime.Value -= Time.deltaTime;

            if (_remainingTime.Value < 0)
            {
                _isCompleteCountdown = true;
            }
        }
    }

    public Countdown(TimeLimit timeLimit)
    {
        _remainingTime.Value = timeLimit.Value;
    }

    public void StartCountdown()
    {
        _isCountdown = true;
    }

    public void AddTime(TimeLimit timeLimit)
    {
        _remainingTime.Value += timeLimit.Value;
    }
}
