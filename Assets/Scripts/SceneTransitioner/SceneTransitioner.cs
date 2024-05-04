using Cysharp.Threading.Tasks;
using System;
using UnityEngine.SceneManagement;

public class SceneTransitioner
{
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

    /// <summary>
    /// �u���b�N�C�������āA�V�[���J�ڂ���������
    /// </summary>
    public async UniTask CompleteTransitionSceneAndBlackIn()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
        await UniTask.WhenAll(OnStartBlackIn());
        await UniTask.WhenAll(OnCompleteBlackIn());
    }

    /// <summary>
    /// �u���b�N�A�E�g�����āA�V�[����J�ڂ���
    /// </summary>
    /// <param name="scene">�J�ڐ�V�[��</param>
    public async UniTask StartTransitionSceneAndBlackOut(SceneEnum scene)
    {
        await UniTask.WhenAll(OnStartBlackOut());
        //await UniTask.WhenAll(OnCompleteBlackOut()); // �g���Ă��Ȃ�
        SceneManager.LoadScene(scene.ToString());
    }
}
