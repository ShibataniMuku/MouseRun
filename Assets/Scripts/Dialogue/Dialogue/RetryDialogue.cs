using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryDialogue : Dialogue, IDialogue
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    /// <summary>
    /// プレイを初めからやり直す
    /// </summary>
    public void Retry()
    {
        AudioManager.audioManagerInstance.StackBgm(BgmEnum.title);
        SceneTransitioner.sceneTransitionerInstance.TransitionNextScene(SceneEnum.Main);
    }
}
