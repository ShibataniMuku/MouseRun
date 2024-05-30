using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class BackgroundPresenter : MonoBehaviour
{
    [SerializeField, Tooltip("背景の目隠し")]
    private BackgroundView _backgroundView;
    [SerializeField, Tooltip("ダイアログの開閉状況を管理するDialogueManager")]
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
