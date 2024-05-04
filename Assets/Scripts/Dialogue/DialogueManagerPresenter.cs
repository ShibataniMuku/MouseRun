using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class DialogueManagerPresenter : MonoBehaviour
{
    [SerializeField, Tooltip("�w�i�{�^�����܂ށA�ʂ̃_�C�A���O")]
    private List<Dialogue> _dialogueView = new List<Dialogue>();
    [SerializeField, Tooltip("�_�C�A���O�̊J�󋵂��Ǘ�����DialogueManager")]
    private DialogueManager _dialogueModel;

    // Start is called before the first frame update
    void Start()
    {
        // �_�C�A���O���J���ꂽ���Ƃ�Model�ɒʒm
        foreach(Dialogue dialogue in _dialogueView)
        {
            dialogue.IsOpen
                .SkipLatestValueOnSubscribe()
                .Subscribe(x =>
                {
                    _dialogueModel.ControllDialogueLayer(x);
                })
                .AddTo(this);
        }

        // �J���Ă���_�C�A���O�̐��ɕύX�����������Ƃ�View�ɒʒm
        _dialogueModel.HasChangedOpeningDialogue
            .SkipLatestValueOnSubscribe()
            .Subscribe(x =>
            {
                foreach (var dialogue in _dialogueView)
                {
                    dialogue.ControllDialogueLayer(x);
                }
            })
            .AddTo(this);
    }
}
