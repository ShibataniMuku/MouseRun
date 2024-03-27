using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UniRx;
using UnityEngine;

public class GameStarterPresenter : MonoBehaviour
{
    [SerializeField]
    private GameStarterView gameStarterView;

    // Start is called before the first frame update
    void Start()
    {
        SceneTransitioner.sceneTransitionerInstance.OnCompleteBlackIn += StartGameAnimationHandler;
        PlayingPhase.playingPhaseInstance.OnFinishGame += FinishGameAnimationHandler;
    }

    private async UniTask StartGameAnimationHandler()
    {
        var cts = new CancellationTokenSource();
        var token = cts.Token;

        await gameStarterView.StartGameAnimation(token);
    }

    private async UniTask FinishGameAnimationHandler()
    {
        var cts = new CancellationTokenSource();
        var token = cts.Token;

        await gameStarterView.FinishGameAnimation(token);
    }
}
