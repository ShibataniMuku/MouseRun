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
        SceneTransitioner.sceneTransitionerInstance.OnCompleteBlackIn += ShowGameViewEventHandler;
    }

    private async UniTask ShowGameViewEventHandler()
    {
        var cts = new CancellationTokenSource();
        var token = cts.Token;

        await gameStarterView.Countdown(token);
    }
}
