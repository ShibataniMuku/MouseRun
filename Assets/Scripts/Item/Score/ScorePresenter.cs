using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class ScorePresenter : MonoBehaviour
{
    [SerializeField]
    private ScoreView  _scoreView;

    // Start is called before the first frame update
    void Start()
    {
        ScoreManager.scoreManagerInstance.Score
            .SkipLatestValueOnSubscribe()
            .Subscribe(x =>
            {
                _scoreView.ResetScoreText(x);
            })
            .AddTo(this);
    }
}
