using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToTitleDialogue : Dialogue, IDialogue
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    /// <summary>
    /// �^�C�g���V�[���ɑJ��
    /// </summary>
    public void GoToTitle()
    {
        SceneTransitioner.sceneTransitionerInstance.TransitionNextScene(SceneEnum.Title);
    }
}
