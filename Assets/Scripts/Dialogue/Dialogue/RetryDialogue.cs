using Zenject;

public class RetryDialogue : Dialogue, IDialogue
{
    [Inject]
    private SceneTransitioner _sceneTransitioner;
    [Inject]
    private AudioManager _audioManager;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    /// <summary>
    /// ƒvƒŒƒC‚ğ‰‚ß‚©‚ç‚â‚è’¼‚·
    /// </summary>
    public void Retry()
    {
        _audioManager.StackBgm(BgmEnum.title);
        _sceneTransitioner.StartTransitionSceneAndBlackOut(SceneEnum.Main);
    }
}
