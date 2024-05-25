using DG.Tweening;
using UniRx;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [SerializeField, Tooltip("背景ボタンを除く、ダイアログ本体")]
    private GameObject _dialoguePanel;
    [SerializeField, Tooltip("背景のバックボタン")]
    private GameObject _backButton;

    private float _defaultScale;
    private RectTransform _dialogueRect;
    private Canvas _dialogueCanvas;
    private int _myLayer = 0; // このダイアログが何階層目に位置しているか

    public IReadOnlyReactiveProperty<bool> IsOpen => _isOpen;
    private readonly BoolReactiveProperty _isOpen = new BoolReactiveProperty(false);

    // Start is called before the first frame update
    protected virtual void Start()
    {
        // ダイアログの元々の大きさを保存
        _dialogueRect = _dialoguePanel.GetComponent<RectTransform>();
        _dialogueCanvas = _dialoguePanel.GetComponent<Canvas>();
        _defaultScale = _dialogueRect.localScale.x;

        InitDialogue();
    }

    public void OpenDialogue()
    {
        Debug.Log("ダイアログを開きます");
        _isOpen.SetValueAndForceNotify(true);
        _dialogueCanvas.enabled = true;
        _backButton.SetActive(true);
        _dialogueRect.localScale = Vector2.one * 10;

        // ダイアログを開くアニメーション
        _dialogueRect.DOScale(_defaultScale, GetDialogueData()._dialogueAnimDuration)
            .SetUpdate(true);
        _dialoguePanel.GetComponent<CanvasGroup>().DOFade(1, GetDialogueData()._dialogueAnimDuration)
            .SetUpdate(true);
    }

    public void CloseDialogue()
    {
        Debug.Log("ダイアログを閉じます");
        _isOpen.SetValueAndForceNotify(false);
        _backButton.SetActive(false);

        // ダイアログを閉じるアニメーション
        _dialogueRect.DOScale(10, GetDialogueData()._dialogueAnimDuration)
            .SetUpdate(true);
        _dialoguePanel.GetComponent<CanvasGroup>().DOFade(0, GetDialogueData()._dialogueAnimDuration)
            .SetUpdate(true)
            .OnComplete(() => _dialogueCanvas.enabled = false) ;
    }

    /// <summary>
    /// 階層数に応じてダイアログをアニメーション
    /// </summary>
    /// <param name="isOpen">閉じられたか開かれたか</param>
    public void ControllDialogueLayer(bool isOpen)
    {
        // ========この下のコードが汚いので、整理したい==========

        if (isOpen && _isOpen.Value)
        {
            _myLayer++;
        }
        else if(_myLayer > 0)
        {
            _myLayer--;
        }

        if (_myLayer > 1 || (_myLayer == 1 && !isOpen)) TransformDialogue(_myLayer);
    }

    /// <summary>
    /// ダイアログを階層数に応じて変形する
    /// </summary>
    /// <param name="layer">位置している階層</param>
    private void TransformDialogue(int layer)
    {
        _dialogueRect.DOScale( _defaultScale - 0.1f * (layer - 1), GetDialogueData()._dialogueRetreatDuration)
            .SetUpdate(true);

        _dialogueRect.pivot = new Vector2(0.5f, 1);

        DOTween.To(() => 0, (x) =>
        {
            _dialogueRect.offsetMin = new Vector2(0, x);
            _dialogueRect.offsetMax = new Vector2(0, x);
        }, GetDialogueData()._dialogueRetreatDistance * (layer - 1), GetDialogueData()._dialogueRetreatDuration)
            .SetUpdate(true)
            .OnComplete(() => _dialogueRect.pivot = new Vector2(0.5f, 1));

        Debug.Log("今、" + layer + " 階層目");
    }

    /// <summary>
    /// ダイアログを初期化
    /// </summary>
    private void InitDialogue()
    {
        _backButton.SetActive(false);
        _dialoguePanel.SetActive(true);
        _dialoguePanel.GetComponent<CanvasGroup>().alpha = 0;
        _dialogueCanvas.enabled = false;
    }

    /// <summary>
    /// ダイアログの設定を読み込む
    /// </summary>
    /// <returns>設定データ</returns>
    private DialogueData GetDialogueData()
    {
        string path = "DialogueData";
        DialogueData data = Resources.Load<DialogueData>(path);
        return data;
    }
}
