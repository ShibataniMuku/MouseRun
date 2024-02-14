using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitioner : MonoBehaviour
{
    public IReadOnlyReactiveProperty<bool> HasTransitionedScene => _hasTransitionedScene;
    private readonly BoolReactiveProperty _hasTransitionedScene = new BoolReactiveProperty(false); // true:�J�ڑO, false;�J�ڌ�

    public static SceneTransitioner sceneTransitionerInstance;
    [HideInInspector]
    public bool onCompleteBlind = false;

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

        // �V�[�����ǂݍ��܂ꂽ��Ă΂��
        SceneManager.activeSceneChanged += ActiveSceneChanged;
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// �V�[����J�ڂ���
    /// </summary>
    /// <param name="scene">�J�ڐ�V�[��</param>
    public void TransitionScene(SceneEnum scene)
    {
        StartCoroutine(LoadScene(scene));
    }
    IEnumerator LoadScene(SceneEnum scene)
    {
        AnimateSceneTransition(true);
        onCompleteBlind = false;

        yield return new WaitUntil(() => onCompleteBlind);

        SceneManager.LoadScene(scene.ToString());
    }

    /// <summary>
    /// ��ʂ𕢂��A�j���[�V�������Đ�
    /// </summary>
    /// <param name="isBlind">true:�߂�, false;�J����</param>
    private void AnimateSceneTransition(bool isBlind)
    {
        _hasTransitionedScene.SetValueAndForceNotify(isBlind);
    }

    private void ActiveSceneChanged(Scene thisScene, Scene nextScene)
    {
        StartCoroutine(Tmp());
    }
    IEnumerator Tmp()
    {
        yield return new WaitForSeconds(0.3f);
        AnimateSceneTransition(false);
    }
}

public enum SceneEnum // �V�[����
{
    Title,
    Main
}
