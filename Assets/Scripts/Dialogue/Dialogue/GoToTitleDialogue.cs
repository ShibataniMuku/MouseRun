using Zenject;

public class GoToTitleDialogue : Dialogue, IDialogue
{
    [Inject]
    private SceneTransitioner _sceneTransitioner;

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
        _sceneTransitioner.StartTransitionSceneAndBlackOut(SceneEnum.Title);
    }
}
