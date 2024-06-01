using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultPhase : MonoBehaviour, IPhase
{
<<<<<<< Updated upstream
    // Start is called before the first frame update
    void Start()
    {
        
=======
    private SceneTransitioner _sceneTransitioner;
    private InheritorBetweenScenes _inheritorBetweenScenes;
    private GameManager _gameManager;

    // 繧ｹ繧ｳ繧｢縺ｪ縺ｩ縺ｮ陦ｨ遉ｺ蜃ｦ逅�
    public delegate UniTask ShowResultDelegate(ResultInfo resultInfo);
    public event ShowResultDelegate OnShowResult;

    private Score score = new Score(0);
    private Score levelBonus = new Score(0);
    private Score additionalBonus = new Score(0);
    private Coin coin = new Coin(0);
    private int exp = 0;
    private Score highScore = new Score(0);
    private int ranking = 0;

    private ResultPhase(SceneTransitioner sceneTransitioner, InheritorBetweenScenes inheritorBetweenScenes, GameManager gameManager)
    {
        _sceneTransitioner = sceneTransitioner;
        _inheritorBetweenScenes = inheritorBetweenScenes;
        _gameManager = gameManager;
>>>>>>> Stashed changes
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< Updated upstream
        
=======
        score = new Score(_inheritorBetweenScenes.GetInheritedData("score"));
        levelBonus = score.ConvertLevelBonus(_gameManager.GetLevel());
        // additionalBonus = 
        // coin =
        // exp =
        // highScore =
        // ranking = 

        await OnCompleteTransition();
>>>>>>> Stashed changes
    }

    public async UniTask RunPhase()
    {
        // ブラックイン
        await SceneTransitioner.sceneTransitionerInstance.CompleteTransitionScene();

<<<<<<< Updated upstream
=======
        // 繝悶Λ繝�繧ｯ繧､繝ｳ
        await _sceneTransitioner.CompleteTransitionSceneAndBlackIn();
        await UniTask.Delay(500);

        // 邨先棡繧定｡ｨ遉ｺ
        ResultInfo resultInfo = new ResultInfo(score, levelBonus, additionalBonus, coin, 10, new Score(10), 10);
        await OnShowResult(resultInfo);
    }
    
    public async UniTask OnStartTransition()
    {
>>>>>>> Stashed changes

    }
}
