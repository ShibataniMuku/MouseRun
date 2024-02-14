using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class NormalButton : AllButton
{
    [SerializeField, Header("�z�o�[���̊g�嗦")]
    protected float _expandRate = 1.1f;
    [SerializeField, Header("�z�o�[���̃A�j���[�V��������")]
    protected float _expandDuration = 0.2f;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        InitSettings();
    }

    private void InitSettings()
    {
        // �{�^���������ꂽ�ۂ̃C�x���g��o�^
        _button.onClick.AddListener(OnClicked);

        // �{�^���̌��X�̑傫����ۑ�
        _defaultScale = _button.GetComponent<RectTransform>().localScale.x;
    }

    protected override void OnClicked()
    {
        base.OnClicked();

        // �{�^�����������Ƃ��̃A�j���[�V����

    }
}
