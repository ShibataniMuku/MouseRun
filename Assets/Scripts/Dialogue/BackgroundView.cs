using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundView : MonoBehaviour
{
    [SerializeField, Tooltip("�_�C�A���O�̔w�i�ɂ���F�����p�̖ډB��")]
    private GameObject _backgroundColor;
    [SerializeField, Tooltip("�_�C�A���O�̔w�i�ɂ���u���[�p�̖ډB��")]
    private GameObject _backgroundBlur;

    private Image _backGroundColorImage;
    private Image _backGroundBlurImage;

    // Start is called before the first frame update
    void Start()
    {
        _backGroundColorImage = _backgroundColor.GetComponent<Image>();
        _backGroundBlurImage = _backgroundBlur.GetComponent<Image>();

        _backGroundColorImage.color -= new Color(0, 0, 0, 1);
        _backGroundBlurImage.material.SetFloat("_Blur", 0);
    }

    /// <summary>
    /// �_�C�A���O�w�i�̖ډB����\������
    /// </summary>
    /// <param name="isShow">�\�����邩�ۂ�</param>
    public void ShowBackground(bool isClosingAllDialogue)
    {
        if (isClosingAllDialogue)
        {
            // �_�C�A���O�̔w�i�F��ω�
            _backGroundColorImage.DOFade(GetDialogueData()._backgroundAlpha, GetDialogueData()._backgroundAnimDuration)
                .SetUpdate(true);

            // �_�C�A���O�̔w�i�̃u���[���x��ω�
            _backGroundBlurImage.material.SetFloat("_Blur", GetDialogueData()._backgroundBlur);
        }
        else
        {
            _backGroundColorImage.DOFade(0, GetDialogueData()._backgroundAnimDuration)
                .SetUpdate(true);

            // �_�C�A���O�̔w�i�̃u���[���x��ω�
            _backGroundBlurImage.material.SetFloat("_Blur", 0);
        }
    }

    /// <summary>
    /// �_�C�A���O�̐ݒ��ǂݍ���
    /// </summary>
    /// <returns>�ݒ�f�[�^</returns>
    private DialogueData GetDialogueData()
    {
        string path = "DialogueData";
        DialogueData data = Resources.Load<DialogueData>(path) as DialogueData;
        return data;
    }
}
