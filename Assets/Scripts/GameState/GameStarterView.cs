using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
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

    public async UniTask Countdown(CancellationToken token)
    {
        var sequence = DOTween.Sequence()
            .Append(readyTmp.DOFade(1, 0.3f).SetUpdate(true))
            .Join(readyRectTrans.DOScale(1, 1f))
            .Append(readyRectTrans.DOScale(0, 0.1f))
            .Join(readyTmp.DOFade(0, 0.3f))
            .Join(goRectTrans.DOScale(1, 0.3f))
            .Join(goTmp.DOFade(1, 0.3f))
           // .AppendCallback(() => _hasFinishedCountdown.SetValueAndForceNotify(true))  ここで操作可能にする！
            .Append(goRectTrans.DOScale(1.8f, 0.7f))
            .Append(goRectTrans.DOScale(10, 0.4f))
            .Join(goTmp.DOFade(0, 0.3f));

        await UniTask.Delay((int)(sequence.Duration() * 1000)); // Sequenceの再生時間をミリ秒に変換して待つ
    }
}
