using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class TimePresenter : MonoBehaviour
{
    [SerializeField]
    GameManager _timeModel;
    [SerializeField]
    TimeView _timeView;

    // Start is called before the first frame update
    void Start()
    {
        _timeModel.CurrentTime
            .Subscribe(x =>
            {
                _timeView.ResetTimeText(x);
            })
            .AddTo(this);
    }
}
