using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class ScoreItemManagerPresenter : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private ScoreItemManager scoreItemManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager.Score
            .SkipLatestValueOnSubscribe()
            .Subscribe(x =>
            {
                scoreItemManager.WaitGeneratingScoreItem();
            })
            .AddTo(this);
    }
}
