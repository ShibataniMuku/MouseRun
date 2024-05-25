using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueData", menuName = "ScriptableObjects/CreateDialogueParamAsset")]
public class DialogueData : ScriptableObject
{
    [SerializeField, Range(0, 1), Header("�_�C�A���O�̊J����")]
    public float _dialogueAnimDuration = 0.2f;
    [SerializeField, Range(0, 1), Header("�_�C�A���O�̌�ގ���")]
    public float _dialogueRetreatDuration = 0.4f;
    [SerializeField, Range(0, 200), Header("�_�C�A���O�̌�ދ���")]
    public float _dialogueRetreatDistance = 100;
    [SerializeField, Range(0, 1), Header("�w�i�̖ډB���̓����x")]
    public float _backgroundAlpha = 0.5f;
    [SerializeField, Range(0, 100), Header("�w�i�̃u���[���x")]
    public float _backgroundBlur = 30;
    [SerializeField, Range(0, 1), Header("�w�i�̖ډB���̏o������")]
    public float _backgroundAnimDuration = 0.4f;
}
