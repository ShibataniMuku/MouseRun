using UniRx;
using UnityEngine;

public class BackgroundPresenter : MonoBehaviour
{
    [SerializeField, Tooltip("ダイアログの後ろの背景")]
    private BackgroundView _backgroundView;
    [SerializeField, Tooltip("ダイアログの開閉状況を管理するDialogueManager")]
    private DialogueManager _dialogueManager;

    // Start is called before the first frame update
    void Start()
    {
        // ダイアログの開閉状況を、後ろの背景に通知する
        _dialogueManager.IsOpeningDialogue
            .SkipLatestValueOnSubscribe()
            .Subscribe(x =>
            {
                _backgroundView.ShowBackground(x);
            })
            .AddTo(this);
    }
}
