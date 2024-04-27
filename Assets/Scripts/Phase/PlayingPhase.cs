using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;

public class PlayingPhase : MonoBehaviour, IPhase
{
    [SerializeField, Header("��������")]
    private const float DEFAULT_TIME = 60;

    public static PlayingPhase playingPhaseInstance;

    public Countdown Countdown => _countdown;
    private Countdown _countdown = new Countdown(new TimeLimit(DEFAULT_TIME));

    // �Q�[���J�n���ɌĂ΂��
    public delegate UniTask StartGameDelegate();
    public event StartGameDelegate OnStartGame;

    // �Q�[���I�����ɌĂ΂��
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

        // �u���b�N�C��
        await SceneTransitioner.sceneTransitionerInstance.CompleteTransitionScene();

        _countdown.StartCountdown();

        Debug.Log("�Q�[���X�^�[�g�I");
        await OnStartGame();

        await UniTask.WaitUntil(() => _countdown.IsCompleteCountdown);

        Debug.Log("�Q�[���I���I");

        await OnFinishGame();

        await UniTask.Delay(3000);

        if (!_canExtendGame)
        {
            // �����s�\�Ȃ�A���U���g��ʂ֑J��

            //InheritorBetweenScenes.inheritorBetweenScenesInstance.SetInheritedData("Score", ScoreManager.scoreManagerInstance.Score.Value.Value);
            
            SceneTransitioner.sceneTransitionerInstance.TransitionNextScene(SceneEnum.Result);
        }
    }

    public async UniTask OnStartTransition()
    {

    }

    public void SetOnStartGame(StartGameDelegate method){ OnStartGame += method; } // �Q�[���J�n���̃f���Q�[�g�ɒǉ�
    public void SetOnFinishGame(FinishGameDelegate method){ OnFinishGame += method; } // �Q�[���I�����̃f���Q�[�g�ɒǉ�
}
