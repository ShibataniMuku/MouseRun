using UnityEngine;

public class RankingDialoguePresenter : DialoguePresenter
{
    [SerializeField, Tooltip("�w�i�{�^�����܂ށA�����L���O�̃_�C�A���O")]
    private RankingDialogue _rankingDialogueModel;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        SetDialogue(_rankingDialogueModel);
    }
}
