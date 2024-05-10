using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using Zenject;

public class PlayingPhase : IPhase, IInitializable
{
    [SerializeField, Header("��������")]
    private const float DEFAULT_TIME = 10;

    private TimeManager _timeManager;
    private SceneTransitioner _sceneTransitioner;
    private InheritorBetweenScenes _inheritorBetweenScenes;
    private ScoreManager _scoreManager;
    private AudioManager _audioManager;

    // �Q�[���J�n���ɌĂ΂��
    public delegate UniTask StartGameDelegate();
    public event StartGameDelegate OnStartGame;

    // �Q�[���I�����ɌĂ΂��
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

        // �u���b�N�C��
        await _sceneTransitioner.CompleteTransitionSceneAndBlackIn();

        _timeManager.MainTimer.StartCountdown();

        Debug.Log("�Q�[���X�^�[�g�I");
        await OnStartGame();

        await UniTask.WaitUntil(() => _timeManager.MainTimer.IsCompleteCountdown);

        Debug.Log("�Q�[���I���I");

        await OnFinishGame();

        await UniTask.Delay(3000);

        if (!_canExtendGame)
        {
            // �����s�\�Ȃ�A���U���g��ʂ֑J��

            _inheritorBetweenScenes.SetInheritedData("score", _scoreManager.CurerntScore.Value.Value);

            _audioManager.StopBGM(1);
            _audioManager.StackBgm(BgmEnum.result);
            _sceneTransitioner.StartTransitionSceneAndBlackOut(SceneEnum.Result);
        }
    }

    public async UniTask OnStartTransition()
    {

    }

    public void SetOnStartGame(StartGameDelegate method) { OnStartGame += method; } // �Q�[���J�n���̃f���Q�[�g�ɒǉ�
    public void SetOnFinishGame(FinishGameDelegate method) { OnFinishGame += method; } // �Q�[���I�����̃f���Q�[�g�ɒǉ�
}
