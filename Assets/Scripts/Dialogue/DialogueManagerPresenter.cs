using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class DialogueManagerPresenter : MonoBehaviour
{
    [SerializeField, Tooltip("背景ボタンを含む、個別のダイアログ")]
    private List<Dialogue> _dialogueView = new List<Dialogue>();
    [SerializeField, Tooltip("ダイアログの開閉状況を管理するDialogueManager")]
    private DialogueManager _dialogueModel;

    // Start is called before the first frame update
    void Start()
    {
        // ダイアログが開閉されたことをModelに通知
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

        // 開いているダイアログの数に変更があったことをViewに通知
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
