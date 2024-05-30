using DG.Tweening;
using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField]
    private GameObject _scoreText;

    private TextMeshProUGUI _scoreTmp;

    // Start is called before the first frame update
    void Start()
    {
        _scoreTmp = _scoreText.GetComponent<TextMeshProUGUI>();
        _scoreTmp.text = "0";
    }

    /// <summary>
    /// スコア表示を更新する
    /// </summary>
    public void ResetScoreText(Score score)
    {
        _scoreText.GetComponent<TextMeshProUGUI>().text = score.Value.ToString();
        _scoreText.transform.localScale = Vector3.one * 1.1f;
        _scoreText.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBounce);
    }
}
