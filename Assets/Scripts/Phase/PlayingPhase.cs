using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayingPhase : MonoBehaviour, IPhase
{
    [SerializeField, Header("��������")]
    private const float DEFAULT_TIME = 60;

    public Countdown Countdown => _countdown;
    private Countdown _countdown;

    private void Awake()
    {
        _countdown = new Countdown(new TimeLimit(DEFAULT_TIME));
    }

    // Start is called before the first frame update
    async void Start()
    {
        await OnCompleteTransition();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("remainingTime: " + _countdown.RemainingTime.Value);
    }

    public async UniTask OnCompleteTransition()
    {
        PipeManager.InformCanRotateable(false);

        await UniTask.Delay(TimeSpan.FromSeconds(0.2f));

        // �u���b�N�C��
        await SceneTransitioner.sceneTransitionerInstance.CompleteTransitionScene();

        PipeManager.InformCanRotateable(true);
        _countdown.StartCountdown();

        Debug.Log("�Q�[���X�^�[�g�I");

        await UniTask.WaitUntil(() => _countdown.IsCompleteCountdown);

        Debug.Log("�Q�[���I���I");
        PipeManager.InformCanRotateable(false);
    }

    public async UniTask OnStartTransition()
    {

    }
}
