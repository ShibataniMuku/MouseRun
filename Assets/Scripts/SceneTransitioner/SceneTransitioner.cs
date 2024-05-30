using Cysharp.Threading.Tasks;
using System;
using UnityEngine.SceneManagement;

public class SceneTransitioner
{
    // ブラックイン開始時に呼ばれる
    public delegate UniTask StartBlackInDelegate();
    public event StartBlackInDelegate OnStartBlackIn;

    // ブラックイン完了時に呼ばれる
    public delegate UniTask CompleteBlackInDelegate();
    public event CompleteBlackInDelegate OnCompleteBlackIn;

    // ブラックアウト開始時に呼ばれる
    public delegate UniTask StartBlackOutDelegate();
    public event StartBlackOutDelegate OnStartBlackOut;

    // ブラックアウト完了時に呼ばれる
    public delegate UniTask CompleteBlackOutDelegate();
    public event CompleteBlackOutDelegate OnCompleteBlackOut;

    /// <summary>
    /// ブラックインさせて、シーン遷移を完了する
    /// </summary>
    public async UniTask CompleteTransitionSceneAndBlackIn()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
        await UniTask.WhenAll(OnStartBlackIn());
        await UniTask.WhenAll(OnCompleteBlackIn());
    }

    /// <summary>
    /// ブラックアウトさせて、シーンを遷移する
    /// </summary>
    /// <param name="scene">遷移先シーン</param>
    public async UniTask StartTransitionSceneAndBlackOut(SceneEnum scene)
    {
        await UniTask.WhenAll(OnStartBlackOut());
        //await UniTask.WhenAll(OnCompleteBlackOut()); // 使っていない
        SceneManager.LoadScene(scene.ToString());
    }
}
