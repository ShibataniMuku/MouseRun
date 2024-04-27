using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;

public class PlayingPhase : MonoBehaviour, IPhase
{
    [SerializeField, Header("制限時間")]
    private const float DEFAULT_TIME = 60;

    public static PlayingPhase playingPhaseInstance;

    public Countdown Countdown => _countdown;
    private Countdown _countdown = new Countdown(new TimeLimit(DEFAULT_TIME));

    // ゲーム開始時に呼ばれる
    public delegate UniTask StartGameDelegate();
    public event StartGameDelegate OnStartGame;

    // ゲーム終了時に呼ばれる
    public delegate UniTask FinishGameDelegate();
    public event FinishGameDelegate OnFinishGame;

    private bool _canExtendGame = false;

    private void Awake()
    {
        if (playingPhaseInstance == null)
        {
            playingPhaseInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    async void Start()
    {
        await OnCompleteTransition();
    }

    public async UniTask OnCompleteTransition()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(0.2f));

        // ブラックイン
        await SceneTransitioner.sceneTransitionerInstance.CompleteTransitionScene();

        _countdown.StartCountdown();

        Debug.Log("ゲームスタート！");
        await OnStartGame();

        await UniTask.WaitUntil(() => _countdown.IsCompleteCountdown);

        Debug.Log("ゲーム終了！");

        await OnFinishGame();

        await UniTask.Delay(3000);

        if (!_canExtendGame)
        {
            // 延長不可能なら、リザルト画面へ遷移

            //InheritorBetweenScenes.inheritorBetweenScenesInstance.SetInheritedData("Score", ScoreManager.scoreManagerInstance.Score.Value.Value);
            
            SceneTransitioner.sceneTransitionerInstance.TransitionNextScene(SceneEnum.Result);
        }
    }

    public async UniTask OnStartTransition()
    {

    }

    public void SetOnStartGame(StartGameDelegate method){ OnStartGame += method; } // ゲーム開始時のデリゲートに追加
    public void SetOnFinishGame(FinishGameDelegate method){ OnFinishGame += method; } // ゲーム終了時のデリゲートに追加
}
