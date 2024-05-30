using UnityEngine;
using UniRx;
using Zenject;

public class ResultButtonPresenter : MonoBehaviour
{
    [SerializeField]
    private TransitionToMainButtonView _transitionToMainButtonView;
    [SerializeField]
    private TransitionToTitleButtonView _transitionToTitleButtonView;
    [SerializeField]
    private TransitonToRankingButtonView _transitionToRankingButtonView;
    [SerializeField]
    private ShareResultButtonView _shareResultButtonView;
    [SerializeField]
    private ShareButtonModel _shareButtonModel;

    [Inject]
    private SceneTransitioner _sceneTransitioner;

    private void Start()
    {
        _transitionToMainButtonView.buttonSubject
            .Subscribe(_ => _sceneTransitioner.StartTransitionSceneAndBlackOut(SceneEnum.Main));

        _transitionToTitleButtonView.buttonSubject
            .Subscribe(_ => _sceneTransitioner.StartTransitionSceneAndBlackOut(SceneEnum.Title));

        _transitionToRankingButtonView.buttonSubject
            .Subscribe(_ => _sceneTransitioner.StartTransitionSceneAndBlackOut(SceneEnum.Main));

        _shareResultButtonView.buttonSubject
            .Subscribe(_ =>
            {
                _shareButtonModel.OnClickShareButton();
            });
    }
}
