using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitioner : MonoBehaviour
{
    public static SceneTransitioner sceneTransitionerInstance;

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

    // Start is called before the first frame update
    private void Awake()
    {
        if (sceneTransitionerInstance == null)
        {
            sceneTransitionerInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // シーンの切り替わりを検知
        SceneManager.activeSceneChanged += ActiveSceneChanged;
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// シーンが遷移完了
    /// </summary>
    public async UniTask CompleteTransitionScene()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
        await UniTask.WhenAll(OnStartBlackIn());
        await UniTask.WhenAll(OnCompleteBlackIn());
    }

    /// <summary>
    /// シーンを遷移する
    /// </summary>
    /// <param name="scene">遷移先シーン</param>
    public async UniTask TransitionNextScene(SceneEnum scene)
    {
        await UniTask.WhenAll(OnStartBlackOut());
        //await UniTask.WhenAll(OnCompleteBlackOut());
        SceneManager.LoadScene(scene.ToString());
    }

    private void ActiveSceneChanged(Scene thisScene, Scene nextScene)
    {
        var _ = CompleteTransitionScene();
    }
}
