using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueData", menuName = "ScriptableObjects/CreateDialogueParamAsset")]
public class DialogueData : ScriptableObject
{
    [SerializeField, Range(0, 1), Header("ダイアログの開閉時間")]
    public float _dialogueAnimDuration = 0.2f;
    [SerializeField, Range(0, 1), Header("ダイアログの後退時間")]
    public float _dialogueRetreatDuration = 0.4f;
    [SerializeField, Range(0, 200), Header("ダイアログの後退距離")]
    public float _dialogueRetreatDistance = 100;
    [SerializeField, Range(0, 1), Header("背景の目隠しの透明度")]
    public float _backgroundAlpha = 0.5f;
    [SerializeField, Range(0, 100), Header("背景のブラー強度")]
    public float _backgroundBlur = 30;
    [SerializeField, Range(0, 1), Header("背景の目隠しの出現時間")]
    public float _backgroundAnimDuration = 0.4f;
}
