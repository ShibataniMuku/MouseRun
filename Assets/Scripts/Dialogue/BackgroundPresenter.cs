using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class BackgroundPresenter : MonoBehaviour
{
    [SerializeField, Tooltip("�w�i�̖ډB��")]
    private BackgroundView _backgroundView;
    [SerializeField, Tooltip("�_�C�A���O�̊J�󋵂��Ǘ�����DialogueManager")]
    private DialogueManager _dialogueManager;

    // Start is called before the first frame update
    void Start()
    {
        _dialogueManager.IsOpeningDialogue
            .SkipLatestValueOnSubscribe()
            .Subscribe(x =>
            {
                _backgroundView.ShowBackground(x);
            })
            .AddTo(this);
    }
}
