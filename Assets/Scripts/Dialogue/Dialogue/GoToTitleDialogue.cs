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
    /// タイトルシーンに遷移
    /// </summary>
    public void GoToTitle()
    {
        SceneTransitioner.sceneTransitionerInstance.TransitionNextScene(SceneEnum.Title);
    }
}
