using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using Zenject;

public class PlayingPhase : IPhase, IInitializable
{
    [SerializeField, Header("制限時間")]
    private const float DEFAULT_TIME = 10;

    private TimeManager _timeManager;
    private SceneTransitioner _sceneTransitioner;
    private InheritorBetweenScenes _inheritorBetweenScenes;
    private ScoreManager _scoreManager;
    private AudioManager _audioManager;

    // ゲーム開始時に呼ばれる
    public delegate UniTask StartGameDelegate();
    public event StartGameDelegate OnStartGame;

    // ゲーム終了時に呼ばれる
    public delegate UniTask FinishGameDelegate();
    public event FinishGameDelegate OnFinishGame;

    private bool _canExtendGame = false;

    PlayingPhase(TimeManager timeManager, SceneTransitioner sceneTransitioner, InheritorBetweenScenes inheritorBetweenScenes, ScoreManager scoreManager, AudioManager audioManager)
    {
        _timeManager = timeManager;
        _sceneTransitioner = sceneTransitioner;
        _inheritorBetweenScenes = inheritorBetweenScenes;
        _scoreManager = scoreManager;
        _audioManager = audioManager;
        _timeManager.MainTimer.SetTimeLimit(new TimeLimit(DEFAULT_TIME));
    }

    public async void Initialize()
    {
        await OnCompleteTransition();
    }

    public async UniTask OnCompleteTransition()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(0.2f));

        // ブラックイン
        await _sceneTransitioner.CompleteTransitionSceneAndBlackIn();

        _timeManager.MainTimer.StartCountdown();

        Debug.Log("ゲームスタート！");
        await OnStartGame();

        await UniTask.WaitUntil(() => _timeManager.MainTimer.IsCompleteCountdown);

        Debug.Log("ゲーム終了！");

        await OnFinishGame();

        await UniTask.Delay(3000);

        if (!_canExtendGame)
        {
            // 延長不可能なら、リザルト画面へ遷移

            _inheritorBetweenScenes.SetInheritedData("score", _scoreManager.CurerntScore.Value.Value);

            _audioManager.StopBGM(1);
            _audioManager.StackBgm(BgmEnum.result);
            _sceneTransitioner.StartTransitionSceneAndBlackOut(SceneEnum.Result);
        }
    }

    public async UniTask OnStartTransition()
    {

    }

    public void SetOnStartGame(StartGameDelegate method) { OnStartGame += method; } // ゲーム開始時のデリゲートに追加
    public void SetOnFinishGame(FinishGameDelegate method) { OnFinishGame += method; } // ゲーム終了時のデリゲートに追加
}
