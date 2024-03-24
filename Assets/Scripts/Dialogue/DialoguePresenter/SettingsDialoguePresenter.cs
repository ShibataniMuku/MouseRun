using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsDialoguePresenter : DialoguePresenter
{
    [SerializeField, Tooltip("�w�i�{�^�����܂ށA�ݒ�̃_�C�A���O")]
    private SettingsDialogue _settingsDialogueModel;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        SetDialogue(_settingsDialogueModel);
    }
}
