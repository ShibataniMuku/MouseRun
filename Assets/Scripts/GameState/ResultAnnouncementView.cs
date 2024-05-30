using TMPro;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class ResultAnnouncementView : MonoBehaviour
{
    [SerializeField, Tooltip("最終的なスコアのテキスト")]
    private GameObject _scoreText;

    [SerializeField, Tooltip("加算された特別ボーナスのテキスト")]
    private GameObject _levelBonusText;
    [SerializeField, Tooltip("加算されたボーナスのテキスト")]
    private GameObject _additionalBonusText;

    [SerializeField, Tooltip("獲得コインのテキスト")]
    private GameObject _gotCoinText;
    [SerializeField, Tooltip("獲得経験値のテキスト")]
    private GameObject _gotExpText;

    [SerializeField, Tooltip("ハイスコアのテキスト")]
    private GameObject _highScoreText;
    [SerializeField, Tooltip("ランキングのテキスト")]
    private GameObject _rankingText;

    private TextMeshProUGUI _scoreTmp;
    private TextMeshProUGUI _levelBonusTmp;
    private TextMeshProUGUI _additionalBonusTmp;
    private TextMeshProUGUI _gotCoinTmp;
    private TextMeshProUGUI _gotExpTmp;
    private TextMeshProUGUI _highScoreTmp;
    private TextMeshProUGUI _rankingTmp;

    private void Awake()
    {
        _scoreTmp = _scoreText.GetComponent<TextMeshProUGUI>();
        _levelBonusTmp = _levelBonusText.GetComponent<TextMeshProUGUI>();
        _additionalBonusTmp = _additionalBonusText.GetComponent<TextMeshProUGUI>();
        _gotCoinTmp = _gotCoinText.GetComponent<TextMeshProUGUI>();
        _gotExpTmp = _gotExpText.GetComponent<TextMeshProUGUI>();
        _highScoreTmp = _highScoreText.GetComponent<TextMeshProUGUI>();
        _rankingTmp = _rankingText.GetComponent<TextMeshProUGUI>();
    }

    public async UniTask ShowResult(ResultInfo resultInfo)
    {
        _scoreTmp.text = resultInfo.score.Value.ToString();
        _levelBonusTmp.text = resultInfo.levelBonus.ToString();
        _additionalBonusTmp.text = resultInfo.additionalBonus.ToString();
        _gotCoinTmp.text = resultInfo.gotCoin.ToString();
        _gotExpTmp.text = resultInfo.gotExp.ToString();
        _highScoreTmp.text = resultInfo.highScore.ToString();
        _rankingTmp.text = resultInfo.ranking.ToString();

        // アニメーションを記述

    }
}
