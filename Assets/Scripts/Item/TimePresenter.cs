using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class TimePresenter : MonoBehaviour
{
    [SerializeField]
    PlayingPhase _timeModel;
    [SerializeField]
    TimeView _timeView;

    // Start is called before the first frame update
    void Start()
    {
        _timeModel.Countdown.RemainingTime
            .Subscribe(x =>
            {
                _timeView.ResetTimeText(x);
            })
            .AddTo(this);
    }
}
