using DG.Tweening;
using UniRx;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [SerializeField, Tooltip("�w�i�{�^���������A�_�C�A���O�{��")]
    private GameObject _dialoguePanel;
    [SerializeField, Tooltip("�w�i�̃o�b�N�{�^��")]
    private GameObject _backButton;

    private float _defaultScale;
    private RectTransform _dialogueRect;
    private Canvas _dialogueCanvas;
    private int _myLayer = 0; // ���̃_�C�A���O�����K�w�ڂɈʒu���Ă��邩

    public IReadOnlyReactiveProperty<bool> IsOpen => _isOpen;
    private readonly BoolReactiveProperty _isOpen = new BoolReactiveProperty(false);

    // Start is called before the first frame update
    protected virtual void Start()
    {
        // �_�C�A���O�̌��X�̑傫����ۑ�
        _dialogueRect = _dialoguePanel.GetComponent<RectTransform>();
        _dialogueCanvas = _dialoguePanel.GetComponent<Canvas>();
        _defaultScale = _dialogueRect.localScale.x;

        InitDialogue();
    }

    public void OpenDialogue()
    {
        Debug.Log("�_�C�A���O���J���܂�");
        _isOpen.SetValueAndForceNotify(true);
        _dialogueCanvas.enabled = true;
        _backButton.SetActive(true);
        _dialogueRect.localScale = Vector2.one * 10;

        // �_�C�A���O���J���A�j���[�V����
        _dialogueRect.DOScale(_defaultScale, GetDialogueData()._dialogueAnimDuration)
            .SetUpdate(true);
        _dialoguePanel.GetComponent<CanvasGroup>().DOFade(1, GetDialogueData()._dialogueAnimDuration)
            .SetUpdate(true);
    }

    public void CloseDialogue()
    {
        Debug.Log("�_�C�A���O����܂�");
        _isOpen.SetValueAndForceNotify(false);
        _backButton.SetActive(false);

        // �_�C�A���O�����A�j���[�V����
        _dialogueRect.DOScale(10, GetDialogueData()._dialogueAnimDuration)
            .SetUpdate(true);
        _dialoguePanel.GetComponent<CanvasGroup>().DOFade(0, GetDialogueData()._dialogueAnimDuration)
            .SetUpdate(true)
            .OnComplete(() => _dialogueCanvas.enabled = false) ;
    }

    /// <summary>
    /// �K�w���ɉ����ă_�C�A���O���A�j���[�V����
    /// </summary>
    /// <param name="isOpen">����ꂽ���J���ꂽ��</param>
    public void ControllDialogueLayer(bool isOpen)
    {
        // ========���̉��̃R�[�h�������̂ŁA����������==========

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
    /// �_�C�A���O���K�w���ɉ����ĕό`����
    /// </summary>
    /// <param name="layer">�ʒu���Ă���K�w</param>
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

        Debug.Log("���A" + layer + " �K�w��");
    }

    /// <summary>
    /// �_�C�A���O��������
    /// </summary>
    private void InitDialogue()
    {
        _backButton.SetActive(false);
        _dialoguePanel.SetActive(true);
        _dialoguePanel.GetComponent<CanvasGroup>().alpha = 0;
        _dialogueCanvas.enabled = false;
    }

    /// <summary>
    /// �_�C�A���O�̐ݒ��ǂݍ���
    /// </summary>
    /// <returns>�ݒ�f�[�^</returns>
    private DialogueData GetDialogueData()
    {
        string path = "DialogueData";
        DialogueData data = Resources.Load<DialogueData>(path);
        return data;
    }
}
