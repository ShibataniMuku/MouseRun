using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UniRx;
using UnityEngine;

public class BlinderPresenter : MonoBehaviour
{
    [SerializeField, Tooltip("シーン遷移時の目隠し")]
    private BlinderView _blinderView;
    [SerializeField, Tooltip("シーン遷移を管理するSceneTransitioner")]
    private SceneTransitioner _blinderModel;

    // Start is called before the first frame update
    void Start()
    {
        _blinderModel.OnStartBlackOut += StartBlackOutHandler;
        _blinderModel.OnStartBlackIn += StartBlackInHandler;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// ブラックアウト開始時に呼ばれる
    /// </summary>
    /// <returns></returns>
    private async UniTask StartBlackOutHandler()
    {
        var cts = new CancellationTokenSource();
        var token = cts.Token;

        await _blinderView.CircleOut(token);
    }

    /// <summary>
    /// ブラックイン開始時に呼ばれる
    /// </summary>
    /// <returns></returns>
    private async UniTask StartBlackInHandler()
    {
        var cts = new CancellationTokenSource();
        var token = cts.Token;

        await _blinderView.CircleIn(token);
    }
}
