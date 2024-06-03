using Cysharp.Threading.Tasks;
using System;
using Zenject;

public class ResultPhase : IPhase, IInitializable
{
    private SceneTransitioner _sceneTransitioner;
    private InheritorBetweenScenes _inheritorBetweenScenes;

    // スコアなどの表示処理
    public delegate UniTask ShowResultDelegate(ResultInfo resultInfo);
    public event ShowResultDelegate OnShowResult;

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
        await UniTask.Delay(500);

        // 結果を表示
        ResultInfo resultInfo = new ResultInfo(new Score(score), score, 10, 10, 10, new Score(10), 10);
        await OnShowResult(resultInfo);
    }
    
    public async UniTask OnStartTransition()
    {

    }
}
