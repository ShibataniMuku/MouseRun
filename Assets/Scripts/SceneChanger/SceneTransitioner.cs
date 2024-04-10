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

    // �u���b�N�C���J�n���ɌĂ΂��
    public delegate UniTask StartBlackInDelegate();
    public event StartBlackInDelegate OnStartBlackIn;

    // �u���b�N�C���������ɌĂ΂��
    public delegate UniTask CompleteBlackInDelegate();
    public event CompleteBlackInDelegate OnCompleteBlackIn;

    // �u���b�N�A�E�g�J�n���ɌĂ΂��
    public delegate UniTask StartBlackOutDelegate();
    public event StartBlackOutDelegate OnStartBlackOut;

    // �u���b�N�A�E�g�������ɌĂ΂��
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

        // �V�[���̐؂�ւ������m
        SceneManager.activeSceneChanged += ActiveSceneChanged;
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// �V�[�����J�ڊ���
    /// </summary>
    public async UniTask CompleteTransitionScene()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
        await UniTask.WhenAll(OnStartBlackIn());
        await UniTask.WhenAll(OnCompleteBlackIn());
    }

    /// <summary>
    /// �V�[����J�ڂ���
    /// </summary>
    /// <param name="scene">�J�ڐ�V�[��</param>
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
