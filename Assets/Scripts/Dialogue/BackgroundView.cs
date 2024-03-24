using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundView : MonoBehaviour
{
    [SerializeField, Tooltip("ダイアログの背景にある目隠し")]
    private GameObject _background;

    // Start is called before the first frame update
    void Start()
    {
        _background.GetComponent<Image>().color -= new Color(0, 0, 0, 1);
    }

    /// <summary>
    /// ダイアログ背景の目隠しを表示する
    /// </summary>
    /// <param name="isShow">表示するか否か</param>
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
