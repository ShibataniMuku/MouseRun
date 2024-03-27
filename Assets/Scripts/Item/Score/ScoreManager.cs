using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager scoreManagerInstance;

    public IReadOnlyReactiveProperty<Score> Score => _score;
    private readonly ReactiveProperty<Score> _score = new ReactiveProperty<Score>(new Score(0));

    private void Awake()
    {
        if (scoreManagerInstance == null)
        {
            scoreManagerInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(Score score)
    {
        _score.Value = _score.Value.AddScore(score);
    }
}
