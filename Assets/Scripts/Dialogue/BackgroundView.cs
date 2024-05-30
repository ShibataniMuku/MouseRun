using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundView : MonoBehaviour
{
    [SerializeField, Tooltip("ダイアログの背景にある色着け用の目隠し")]
    private GameObject _backgroundColor;
    [SerializeField, Tooltip("ダイアログの背景にあるブラー用の目隠し")]
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
    /// ダイアログ背景の目隠しを表示する
    /// </summary>
    /// <param name="isShow">表示するか否か</param>
    public void ShowBackground(bool isClosingAllDialogue)
    {
        if (isClosingAllDialogue)
        {
            // ダイアログの背景色を変化
            _backGroundColorImage.DOFade(GetDialogueData()._backgroundAlpha, GetDialogueData()._backgroundAnimDuration)
                .SetUpdate(true);

            // ダイアログの背景のブラー強度を変化
            _backGroundBlurImage.material.SetFloat("_Blur", GetDialogueData()._backgroundBlur);
        }
        else
        {
            _backGroundColorImage.DOFade(0, GetDialogueData()._backgroundAnimDuration)
                .SetUpdate(true);

            // ダイアログの背景のブラー強度を変化
            _backGroundBlurImage.material.SetFloat("_Blur", 0);
        }
    }

    /// <summary>
    /// ダイアログの設定を読み込む
    /// </summary>
    /// <returns>設定データ</returns>
    private DialogueData GetDialogueData()
    {
        string path = "DialogueData";
        DialogueData data = Resources.Load<DialogueData>(path) as DialogueData;
        return data;
    }
}
