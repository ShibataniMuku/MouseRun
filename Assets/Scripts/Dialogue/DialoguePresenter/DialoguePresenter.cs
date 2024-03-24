using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

[System.Serializable]
public class DialoguePresenter : MonoBehaviour
{
    [SerializeField, Header("����"), Tooltip("�����͎��s�ɖ��֌W�ł�")]
    private string _memo;
    [Space(5)]

    [SerializeField, Tooltip("�_�C�A���O���J���{�^��")]
    private Button _openingButtonView;
    [SerializeField, Tooltip("�_�C�A���O�����{�^��")]
    private Button _closingButtonView;

    private IDialogue _dialogueModel;

    // Start is called before the first frame update
    public virtual void Start()
    {
        // �J���{�^���������ꂽ���Ƃ�Model���ɒʒm
        _openingButtonView.OnClickAsObservable()
            .Subscribe(x =>
            {
                _dialogueModel.OpenDialogue();
            })
            .AddTo(this);

        // ����{�^���������ꂽ���Ƃ�Model���ɒʒm
        _closingButtonView.OnClickAsObservable()
            .Subscribe(x =>
            {
                _dialogueModel.CloseDialogue();
            })
            .AddTo(this);
    }

    protected void SetDialogue(IDialogue dialogue)
    {
        _dialogueModel = dialogue;
    }
}
