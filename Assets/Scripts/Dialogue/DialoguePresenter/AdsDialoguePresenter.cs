using UnityEngine;

public class AdsDialoguePresenter : DialoguePresenter
{
    [SerializeField, Tooltip("�w�i�{�^�����܂ށA�L���̃_�C�A���O")]
    private AdsDialogue _adsDialogueModel;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        SetDialogue(_adsDialogueModel);
    }
}
