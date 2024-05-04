using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class GoToTitleDialoguePresener : DialoguePresenter
{
    [SerializeField, Tooltip("�^�C�g���ɖ߂�{�^��")]
    private TransitionToTitleButtonView _transitionToTitleButtonView;
    [SerializeField, Tooltip("�w�i�{�^�����܂ށA�^�C�g���ɖ߂�p�̃_�C�A���O")]
    private GoToTitleDialogue _goToTitleDialogueModel;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        SetDialogue(_goToTitleDialogueModel);

        // �^�C�g���ɖ߂�{�^���������ꂽ���Ƃ�Model���ɒʒm
        _transitionToTitleButtonView.buttonSubject
                .Subscribe(x =>
                {
                    _goToTitleDialogueModel.GoToTitle();
                })
                .AddTo(this);
    }
}
