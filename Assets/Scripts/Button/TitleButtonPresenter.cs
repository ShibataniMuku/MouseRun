using UniRx;
using UnityEngine;
using Zenject;

public class TitleButtonPresenter : MonoBehaviour
{
    [SerializeField]
    private BasicButtonView _transition1minSceneButtonView;
    [SerializeField]
    private BasicButtonView _transitionInfinitySceneButtonView;

    [Inject]
    private SceneTransitioner _sceneTransitioner;

    private void Start()
    {
        _transition1minSceneButtonView.buttonSubject
            .Subscribe(_ => _sceneTransitioner.StartTransitionSceneAndBlackOut(SceneEnum.Main));

        _transitionInfinitySceneButtonView.buttonSubject
            .Subscribe(_ => _sceneTransitioner.StartTransitionSceneAndBlackOut(SceneEnum.Main));
    }
}
