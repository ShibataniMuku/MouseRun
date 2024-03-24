using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayingPhase : MonoBehaviour, IPhase
{
    [SerializeField, Header("制限時間")]
    private const float DEFAULT_TIME = 60;

    public Countdown Countdown => _countdown;
    private Countdown _countdown;

    // Start is called before the first frame update
    void Start()
    {
        _countdown = new Countdown(new TimeLimit(DEFAULT_TIME));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public async UniTask OnCompleteTransition()
    {
        PipeManager.InformCanRotateable(false);

        await UniTask.Delay(TimeSpan.FromSeconds(0.2f));

        // ブラックイン
        await SceneTransitioner.sceneTransitionerInstance.CompleteTransitionScene();

        PipeManager.InformCanRotateable(true);
        GameManager.gameManagerInstance._IsCowntdown = true;
        _countdown.StartCountdown();

        Debug.Log("ゲームスタート！");

        await UniTask.WaitUntil(() => _countdown.IsCompleteCountdown);

        Debug.Log("ゲーム終了！");
        PipeManager.InformCanRotateable(false);
        GameManager.gameManagerInstance._IsCowntdown = false;
    }

    public async UniTask OnStartTransition()
    {

    }
}
