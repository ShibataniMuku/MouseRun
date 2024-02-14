using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    [SerializeField, Header("��������")]
    private float _defaultTime = 60;

    private bool _isPlay = true;
    private IEnumerator playGame; // �t�F�[�Y�Ǘ��p�̃R���[�`��

    public IReadOnlyReactiveProperty<float> CurrentTime => _currentTime;
    private readonly FloatReactiveProperty _currentTime = new FloatReactiveProperty(0); // �c�莞��

    public IReadOnlyReactiveProperty<int> Score => _score;
    private readonly IntReactiveProperty _score = new IntReactiveProperty(0); // ���_



    public IReadOnlyReactiveProperty<bool> IsCountdown => _isCountdown;
    private readonly BoolReactiveProperty _isCountdown = new BoolReactiveProperty(false); // �Q�[���J�n�̃J�E���g�_�E���J�n

    public IReadOnlyReactiveProperty<bool> HasFinished => _hasFinished;
    private readonly BoolReactiveProperty _hasFinished = new BoolReactiveProperty(false); // �Q�[�����I��

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
                // ���Ԑ؂�
                _isPlay = false;
                StopCoroutine(playGame);
            }
        }
    }

    /// <summary>
    /// ���Ԃ����Z����
    /// </summary>
    /// <param name="addedTime">���Z���鎞��</param>
    public void AddTime(float addedTime)
    {
        _currentTime.Value += addedTime;
        Debug.Log($"���Ԃ����Z����܂���({addedTime}�b)");
    }

    /// <summary>
    /// ���_�����Z����
    /// </summary>
    /// <param name="addedTime">���Z���鎞��</param>
    public void AddScore(int addedScore)
    {
        _score.Value += addedScore;
        Debug.Log($"���_�����Z����܂���(+{addedScore}�_)");
    }

    /// <summary>
    /// �Q�[���̐i�s�𐧌䂷��
    /// </summary>
    /// <param name="isStart">true:�J�n, false;��~</param>
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
        _isCountdown.SetValueAndForceNotify(true); // �J�E���g�_�E���̃t�F�[�Y�ɓ��������Ƃ�ʒm

        ControllGame(false);

        // �Q�[�����v���C��
        PipeManager.InformCanRotateable(true);

    }
}
