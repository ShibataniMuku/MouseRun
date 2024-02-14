using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class ScorePresenter : MonoBehaviour
{
    [SerializeField]
    private GameManager _scoreModel;
    [SerializeField]
    private ScoreView  _scoreView;

    // Start is called before the first frame update
    void Start()
    {
        _scoreModel.Score
            .SkipLatestValueOnSubscribe()
            .Subscribe(x =>
            {
                _scoreView.ResetScoreText(x);
            })
            .AddTo(this);
    }
}
