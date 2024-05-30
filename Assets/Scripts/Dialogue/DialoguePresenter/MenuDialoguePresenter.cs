using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class MenuDialoguePresenter : DialoguePresenter
{
    [SerializeField, Tooltip("背景ボタンを含む、メニューのダイアログ")]
    private MenuDialogue _menuDialogueModel;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        SetDialogue(_menuDialogueModel);
    }
}
