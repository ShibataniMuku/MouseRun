using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    [SerializeField, Header("制限時間")]
    private float _defaultTime = 60;

    private bool _isPlay = true;
    private IEnumerator playGame; // フェーズ管理用のコルーチン

    public IReadOnlyReactiveProperty<float> CurrentTime => _currentTime;
    private readonly FloatReactiveProperty _currentTime = new FloatReactiveProperty(0); // 残り時間

    public IReadOnlyReactiveProperty<int> Score => _score;
    private readonly IntReactiveProperty _score = new IntReactiveProperty(0); // 得点



    public IReadOnlyReactiveProperty<bool> IsCountdown => _isCountdown;
    private readonly BoolReactiveProperty _isCountdown = new BoolReactiveProperty(false); // ゲーム開始のカウントダウン開始

    public IReadOnlyReactiveProperty<bool> HasFinished => _hasFinished;
    private readonly BoolReactiveProperty _hasFinished = new BoolReactiveProperty(false); // ゲームが終了

    // Start is called before the first frame update
    void Start()
    {
        _currentTime.Value = _defaultTime;

        playGame = PlayGame();
        StartCoroutine(playGame);
    }

    // Update is called once per frame
    void Update()
    {
        if (_isPlay)
        {
            _currentTime.Value -= Time.deltaTime;

            if (_currentTime.Value <= 0)
            {
                // 時間切れ
                _isPlay = false;
                StopCoroutine(playGame);
            }
        }
    }

    /// <summary>
    /// 時間を加算する
    /// </summary>
    /// <param name="addedTime">加算する時間</param>
    public void AddTime(float addedTime)
    {
        _currentTime.Value += addedTime;
        Debug.Log($"時間が加算されました({addedTime}秒)");
    }

    /// <summary>
    /// 得点を加算する
    /// </summary>
    /// <param name="addedTime">加算する時間</param>
    public void AddScore(int addedScore)
    {
        _score.Value += addedScore;
        Debug.Log($"得点が加算されました(+{addedScore}点)");
    }

    /// <summary>
    /// ゲームの進行を制御する
    /// </summary>
    /// <param name="isStart">true:開始, false;停止</param>
    public void ControllGame(bool isStart)
    {
        if (isStart)
        {
            StartCoroutine(playGame);
        }
        else
        {
            StopCoroutine(playGame);
        }
    }

    private IEnumerator PlayGame()
    {
        yield return null;
        PipeManager.InformCanRotateable(false);
        _isCountdown.SetValueAndForceNotify(true); // カウントダウンのフェーズに入ったことを通知

        ControllGame(false);

        // ゲームをプレイ中
        PipeManager.InformCanRotateable(true);

    }
}
