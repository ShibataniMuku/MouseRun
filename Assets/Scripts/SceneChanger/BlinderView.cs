using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class BlinderView : MonoBehaviour
{
    [SerializeField, Range(0, 3), Header("�V�[���J�ڎ���")]
    private float _sceneTransDuration = 1;
    [SerializeField, Header("�V�[���J�ڎ��̖ډB���̃A�j���[�V����")]
    private AnimationCurve _blinderAnimCurve;
    [SerializeField, Tooltip("�V�[���J�ڎ��ɉ�ʂ𕢂��ډB��")]
    private GameObject _blinder;
    [SerializeField, Tooltip("�ډB���𐧌䂷��}�X�N")]
    private GameObject _mask;

    RectTransform _maskRect;

    public IReadOnlyReactiveProperty<bool> OnCompleteTransition => _onCompleteTransition;
    private readonly BoolReactiveProperty _onCompleteTransition = new BoolReactiveProperty(false);

    // Start is called before the first frame update
    void Start()
    {
        _maskRect = _mask.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// ��ʑJ�ڂ̂��߂̃t�F�[�h�C���E�A�E�g
    /// </summary>
    /// <param name="isBlind">�t�F�[�h�A�E�g���ۂ�</param>
    public void SwitchScreenBlinder(bool isBlind)
    {
        if (isBlind)
        {
            // �t�F�[�h�A�E�g
            _blinder.SetActive(true);
            _mask.SetActive(true);
            _maskRect = _mask.GetComponent<RectTransform>();
            _maskRect.sizeDelta = Vector2.one * Screen.height * 1.5f;
            _maskRect.DOSizeDelta(Vector2.zero, _sceneTransDuration)
                .SetEase(_blinderAnimCurve)
                .SetUpdate(true)
                .OnComplete(() =>
                {
                    Time.timeScale = 1;
                    _onCompleteTransition.SetValueAndForceNotify(true);
                });
        }
        else
        {
            // �t�F�[�h�C��
            _blinder.SetActive(true);
            _mask.SetActive(true);
            _maskRect = _mask.GetComponent<RectTransform>();
            _maskRect.sizeDelta = Vector2.zero;
            _maskRect.DOSizeDelta(Vector2.one * Screen.height * 1.5f, _sceneTransDuration)
                .SetEase(_blinderAnimCurve)
                .SetUpdate(true)
                .OnComplete(() =>
                {
                    _blinder.SetActive(false);
                    _mask.SetActive(false);
                    _onCompleteTransition.SetValueAndForceNotify(true);
                });
        }
    }
}
