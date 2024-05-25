using TMPro;
using UnityEngine;

public class ResultAnnouncementView : MonoBehaviour
{
    [SerializeField, Tooltip("�ŏI�I�ȃX�R�A�̃e�L�X�g")]
    private GameObject _scoreText;

    [SerializeField, Tooltip("���Z���ꂽ���ʃ{�[�i�X�̃e�L�X�g")]
    private GameObject _levelBonusText;
    [SerializeField, Tooltip("���Z���ꂽ�{�[�i�X�̃e�L�X�g")]
    private GameObject _additionalBonusText;

    [SerializeField, Tooltip("�l���R�C���̃e�L�X�g")]
    private GameObject _gotCoinText;
    [SerializeField, Tooltip("�l���o���l�̃e�L�X�g")]
    private GameObject _gotExpText;

    [SerializeField, Tooltip("�n�C�X�R�A�̃e�L�X�g")]
    private GameObject _highScoreText;
    [SerializeField, Tooltip("�����L���O�̃e�L�X�g")]
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

        // �A�j���[�V�����Ȃǂ������ɋL�q

    }

    public void SetLevelBonusText(int levelBonus)
    {
        _levelBonusTmp.text = levelBonus.ToString();

        // �A�j���[�V�����Ȃǂ������ɋL�q

    }

    public void SetAdditionalBonusText(int additionalBonus)
    {
        _additionalBonusTmp.text = additionalBonus.ToString();

        // �A�j���[�V�����Ȃǂ������ɋL�q

    }

    public void SetCoinText(int coin)
    {
        _gotCoinTmp.text = coin.ToString();

        // �A�j���[�V�����Ȃǂ������ɋL�q

    }

    public void SetExpText(int exp)
    {
        _gotExpTmp.text = exp.ToString();

        // �A�j���[�V�����Ȃǂ������ɋL�q

    }

    public void SetHighScoreText(Score score)
    {
        _highScoreTmp.text = score.ToString();

        // �A�j���[�V�����Ȃǂ������ɋL�q

    }

    public void SetRankingText(int rank)
    {
        _rankingTmp.text = rank.ToString();

        // �A�j���[�V�����Ȃǂ������ɋL�q

    }
}
