using System.Collections;
using System.Collections.Generic;
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
    public void ResetScoreText(int score)
    {
        _scoreText.GetComponent<TextMeshProUGUI>().text = score.ToString();
    }
}
