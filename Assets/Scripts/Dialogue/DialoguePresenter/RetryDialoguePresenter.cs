using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class RetryDialoguePresenter : DialoguePresenter
{
    [SerializeField, Tooltip("���g���C�{�^��")]
    private Button _retryButtonVIew;
    [SerializeField, Tooltip("�w�i�{�^�����܂ށA���g���C�p�̃_�C�A���O")]
    private RetryDialogue _retryDialogueModel;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        SetDialogue(_retryDialogueModel);

        // ���g���C�{�^���������ꂽ���Ƃ�Model���ɒʒm
        _retryButtonVIew.OnClickAsObservable()
                .Subscribe(x =>
                {
                    _retryDialogueModel.Retry();
                })
                .AddTo(this);
    }
}
