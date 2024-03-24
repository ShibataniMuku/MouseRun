using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UniRx;
using UnityEngine;

public class BlinderPresenter : MonoBehaviour
{
    [SerializeField, Tooltip("�V�[���J�ڎ��̖ډB��")]
    private BlinderView _blinderView;
    [SerializeField, Tooltip("�V�[���J�ڂ��Ǘ�����SceneTransitioner")]
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
    /// �u���b�N�A�E�g�J�n���ɌĂ΂��
    /// </summary>
    /// <returns></returns>
    private async UniTask StartBlackOutHandler()
    {
        var cts = new CancellationTokenSource();
        var token = cts.Token;

        await _blinderView.CircleOut(token);
    }

    /// <summary>
    /// �u���b�N�C���J�n���ɌĂ΂��
    /// </summary>
    /// <returns></returns>
    private async UniTask StartBlackInHandler()
    {
        var cts = new CancellationTokenSource();
        var token = cts.Token;

        await _blinderView.CircleIn(token);
    }
}
