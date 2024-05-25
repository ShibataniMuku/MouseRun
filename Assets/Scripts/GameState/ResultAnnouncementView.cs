using TMPro;
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

    public void SetScoreText(Score score)
    {
        _scoreTmp.text = score.Value.ToString();

        // アニメーションなどをここに記述

    }

    public void SetLevelBonusText(int levelBonus)
    {
        _levelBonusTmp.text = levelBonus.ToString();

        // アニメーションなどをここに記述

    }

    public void SetAdditionalBonusText(int additionalBonus)
    {
        _additionalBonusTmp.text = additionalBonus.ToString();

        // アニメーションなどをここに記述

    }

    public void SetCoinText(int coin)
    {
        _gotCoinTmp.text = coin.ToString();

        // アニメーションなどをここに記述

    }

    public void SetExpText(int exp)
    {
        _gotExpTmp.text = exp.ToString();

        // アニメーションなどをここに記述

    }

    public void SetHighScoreText(Score score)
    {
        _highScoreTmp.text = score.ToString();

        // アニメーションなどをここに記述

    }

    public void SetRankingText(int rank)
    {
        _rankingTmp.text = rank.ToString();

        // アニメーションなどをここに記述

    }
}
