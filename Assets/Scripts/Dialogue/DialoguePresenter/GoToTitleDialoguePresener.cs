using UnityEngine;
using UniRx;

public class GoToTitleDialoguePresener : DialoguePresenter
{
    [SerializeField, Tooltip("タイトルに戻るボタン")]
    private TransitionToTitleButtonView _transitionToTitleButtonView;
    [SerializeField, Tooltip("背景ボタンを含む、タイトルに戻る用のダイアログ")]
    private GoToTitleDialogue _goToTitleDialogueModel;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        SetDialogue(_goToTitleDialogueModel);

        // タイトルに戻るボタンが押されたことをModel側に通知
        _transitionToTitleButtonView.buttonSubject
                .Subscribe(x =>
                {
                    _goToTitleDialogueModel.GoToTitle();
                })
                .AddTo(this);
    }
}
