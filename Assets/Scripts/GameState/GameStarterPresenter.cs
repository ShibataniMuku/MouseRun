using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class GameStarterPresenter : MonoBehaviour
{
    [SerializeField]
    private GameStarterView gameStarterView;
    [SerializeField]
    private GameManager gameStarterModel;

    // Start is called before the first frame update
    void Start()
    {
        gameStarterModel.IsCountdown
            .SkipLatestValueOnSubscribe()
            .Subscribe(x =>
            {
                gameStarterView.Countdown();
            })
            .AddTo(this);

        gameStarterView.HasFinishedCountdown
            .SkipLatestValueOnSubscribe()
            .Subscribe(x =>
            {
                gameStarterModel.ControllGame(true);
            })
            .AddTo(this);
    }
}
