using UnityEngine;

public class AdsDialoguePresenter : DialoguePresenter
{
    [SerializeField, Tooltip("背景ボタンを含む、広告のダイアログ")]
    private AdsDialogue _adsDialogueModel;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        SetDialogue(_adsDialogueModel);
    }
}
