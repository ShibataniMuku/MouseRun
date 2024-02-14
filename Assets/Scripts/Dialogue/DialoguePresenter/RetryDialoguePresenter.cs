using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class RetryDialoguePresenter : DialoguePresenter
{
    [SerializeField, Tooltip("リトライボタン")]
    private Button _retryButtonVIew;
    [SerializeField, Tooltip("背景ボタンを含む、リトライ用のダイアログ")]
    private RetryDialogue _retryDialogueModel;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        SetDialogue(_retryDialogueModel);

        // リトライボタンが押されたことをModel側に通知
        _retryButtonVIew.OnClickAsObservable()
                .Subscribe(x =>
                {
                    _retryDialogueModel.Retry();
                })
                .AddTo(this);
    }
}
