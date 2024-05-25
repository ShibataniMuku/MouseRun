using UnityEngine;

public class RankingDialoguePresenter : DialoguePresenter
{
    [SerializeField, Tooltip("背景ボタンを含む、ランキングのダイアログ")]
    private RankingDialogue _rankingDialogueModel;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        SetDialogue(_rankingDialogueModel);
    }
}
