using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class NormalButton : AllButton
{
    [SerializeField, Header("ホバー時の拡大率")]
    protected float _expandRate = 1.1f;
    [SerializeField, Header("ホバー時のアニメーション時間")]
    protected float _expandDuration = 0.2f;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        InitSettings();
    }

    private void InitSettings()
    {
        // ボタンが押された際のイベントを登録
        _button.onClick.AddListener(OnClicked);

        // ボタンの元々の大きさを保存
        _defaultScale = _button.GetComponent<RectTransform>().localScale.x;
    }

    protected override void OnClicked()
    {
        base.OnClicked();

        // ボタンを押したときのアニメーション

    }
}
