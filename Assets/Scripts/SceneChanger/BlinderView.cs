using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UniRx;
using UnityEngine;

public class BlinderView : MonoBehaviour
{
    [SerializeField, Range(0, 3), Header("シーン遷移時間")]
    private float _sceneTransDuration = 1;
    [SerializeField, Header("シーン遷移時の目隠しのアニメーション")]
    private AnimationCurve _blinderAnimCurve;
    [SerializeField, Tooltip("シーン遷移時に画面を覆う目隠し")]
    private GameObject _blinder;
    [SerializeField, Tooltip("目隠しを制御するマスク")]
    private GameObject _mask;

    RectTransform _maskRect;

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
    /// 円状にブラックイン
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public async UniTask CircleIn(CancellationToken token)
    {
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
            });

        await UniTask.Delay(1000);
    }

    /// <summary>
    /// 円状にブラックアウト
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public async UniTask CircleOut(CancellationToken token)
    {
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
            });

        await UniTask.Delay(1000);
    }
}
