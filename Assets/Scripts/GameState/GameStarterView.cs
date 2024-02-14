using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;

public class GameStarterView : MonoBehaviour
{
    [SerializeField, Tooltip("READY?のテキスト")]
    private GameObject readyText;
    [SerializeField, Tooltip("GO!のテキスト")]
    private GameObject goText;

    private RectTransform readyRectTrans;
    private TextMeshProUGUI readyTmp;
    private RectTransform goRectTrans;
    private TextMeshProUGUI goTmp;

    public IReadOnlyReactiveProperty<bool> HasFinishedCountdown => _hasFinishedCountdown;
    private readonly BoolReactiveProperty _hasFinishedCountdown = new BoolReactiveProperty(false); // カウントダウンが終了

    // Start is called before the first frame update
    void Start()
    {
        readyRectTrans = readyText.GetComponent<RectTransform>();
        readyTmp = readyText.GetComponent<TextMeshProUGUI>();
        goRectTrans = goText.GetComponent<RectTransform>();
        goTmp = goText.GetComponent<TextMeshProUGUI>();

        readyTmp.alpha = 0;
        goTmp.alpha = 0;
        readyRectTrans.localScale = Vector3.one * 2;
    }

    public void Countdown()
    {
        DOTween.Sequence()
            .Append(readyTmp.DOFade(1, 0.3f).SetUpdate(true))
            .Join(readyRectTrans.DOScale(1, 1f))
            .Append(readyRectTrans.DOScale(0, 0.1f))
            .Join(readyTmp.DOFade(0, 0.3f))
            .Join(goRectTrans.DOScale(1, 0.3f))
            .Join(goTmp.DOFade(1, 0.3f))
            .AppendCallback(() => _hasFinishedCountdown.SetValueAndForceNotify(true))
            .Append(goRectTrans.DOScale(1.8f, 0.7f))
            .Append(goRectTrans.DOScale(10, 0.4f))
            .Join(goTmp.DOFade(0, 0.3f));
    }
}
