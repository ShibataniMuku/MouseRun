using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class BackgroundPresenter : MonoBehaviour
{
    [SerializeField, Tooltip("”wŒi‚Ì–Ú‰B‚µ")]
    private BackgroundView _backgroundView;
    [SerializeField, Tooltip("ƒ_ƒCƒAƒƒO‚ÌŠJ•Âó‹µ‚ðŠÇ—‚·‚éDialogueManager")]
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
