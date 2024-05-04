using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using Zenject;

public class ResultPhase : IPhase, IInitializable
{
    private SceneTransitioner _sceneTransitioner;
    private InheritorBetweenScenes _inheritorBetweenScenes;

    private int score;
    private int levelBonus;
    private int additionalBonus;
    private int coin;
    private int exp;
    private int highScore;
    private int ranking;

    private ResultPhase(SceneTransitioner sceneTransitioner, InheritorBetweenScenes inheritorBetweenScenes)
    {
        _sceneTransitioner = sceneTransitioner;
        _inheritorBetweenScenes = inheritorBetweenScenes;
    }

    public async void Initialize()
    {
        score = _inheritorBetweenScenes.GetInheritedData("score");

        await OnCompleteTransition();
    }

    public async UniTask OnCompleteTransition()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(0.2f));

        // ブラックイン
        await _sceneTransitioner.CompleteTransitionSceneAndBlackIn();


    }

    public async UniTask OnStartTransition()
    {

    }
}
