using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundView : MonoBehaviour
{
    [SerializeField, Tooltip("�_�C�A���O�̔w�i�ɂ���ډB��")]
    private GameObject _background;

    // Start is called before the first frame update
    void Start()
    {
        _background.GetComponent<Image>().color -= new Color(0, 0, 0, 1);
    }

    /// <summary>
    /// �_�C�A���O�w�i�̖ډB����\������
    /// </summary>
    /// <param name="isShow">�\�����邩�ۂ�</param>
    public void ShowBackground(bool isClosingAllDialogue)
    {
        if (isClosingAllDialogue)
        {
            _background.GetComponent<Image>().DOFade(GetDialogueData()._backgroundAlpha, GetDialogueData()._backgroundAnimDuration)
                .SetUpdate(true);
        }
        else
        {
            _background.GetComponent<Image>().DOFade(0, GetDialogueData()._backgroundAnimDuration)
                .SetUpdate(true);
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
