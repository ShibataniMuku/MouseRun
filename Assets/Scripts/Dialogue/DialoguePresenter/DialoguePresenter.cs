using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

[System.Serializable]
public class DialoguePresenter : MonoBehaviour
{
    [SerializeField, Header("メモ"), Tooltip("メモは実行に無関係です")]
    private string _memo;
    [Space(5)]

    [SerializeField, Tooltip("ダイアログを開くボタン")]
    private Button _openingButtonView;
    [SerializeField, Tooltip("ダイアログを閉じるボタン")]
    private Button _closingButtonView;

    private IDialogue _dialogueModel;

    // Start is called before the first frame update
    public virtual void Start()
    {
        // 開くボタンが押されたことをModel側に通知
        _openingButtonView.OnClickAsObservable()
            .Subscribe(x =>
            {
                _dialogueModel.OpenDialogue();
            })
            .AddTo(this);

        // 閉じるボタンが押されたことをModel側に通知
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
