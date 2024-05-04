using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [SerializeField, Tooltip("�I�[�f�B�I�~�L�T�[")]
    private AudioMixer _audioMixer;
    [SerializeField, Tooltip("BGM�p��AudioSource")]
    private AudioSource _bgmAudioSource;
    [SerializeField, Tooltip("SE�p��AudioSource")]
    private AudioSource _seAudioSource;

    private AudioData _audioData;
    private BgmEnum stackedBgm;
    private bool _isStacked = false; // BGM���X�^�b�N����Ă��邩�ۂ�

    //public static AudioManager audioManagerInstance;

    private void Awake()
    {
        _audioData = GetAudioData();
    }

    // Start is called before the first frame update
    void Start()
    {
        CheckBgmOverlap(_audioData.bgmData);
        CheckSeOverlap(_audioData.seData);

        SceneManager.activeSceneChanged += PlayStackedBgm;

        // ================ �� �Ƃ肠����BGM���e�X�g�ŗ����Ă��� ==============================
       PlayBGM(BgmEnum.title);
    }

    /// <summary>
    /// �����BGM��o�^���Ă��Ȃ���������
    /// </summary>
    /// <param name="data">�o�^BGM�f�[�^</param>
    private void CheckBgmOverlap(List<BgmClipData> data)
    {
        List<BgmEnum> bgm = new List<BgmEnum>();
        for (int i = 0; i < data.Count; i++)
        {
            if (bgm.Contains(data[i].type))
            {
                Debug.LogError(string.Format($"�T�E���h {data[i].type} ���d�����Ă��܂�"));
            }
            else
            {
                bgm.Add(data[i].type);
            }
        }
    }

    /// <summary>
    /// �����SE��o�^���Ă��Ȃ���������
    /// </summary>
    /// <param name="data">�o�^SE�f�[�^</param>
    private void CheckSeOverlap(List<SeClipData> data)
    {
        List<SeEnum> bgm = new List<SeEnum>();
        for (int i = 0; i < data.Count; i++)
        {
            if (bgm.Contains(data[i].type))
            {
                Debug.LogError(string.Format($"�T�E���h {data[i].type} ���d�����Ă��܂�"));
            }
            else
            {
                bgm.Add(data[i].type);
            }
        }
    }

    /// <summary>
    /// BgmClipData�ɓo�^���ꂽ�T�E���h���AEnum�̏��ɕ��בւ���
    /// </summary>
    /// <param name="data">BGM�̓o�^�f�[�^</param>
    /// <param name="bgm">�T������BGM��Enum</param>
    private int ConvertBgmEnumIntoIndex(List<BgmClipData> data, BgmEnum bgm)
    {
        for (int i = 0; i < data.Count; i++)
        {
            if (bgm == data[i].type) return i;
        }

        Debug.LogError(string.Format($"�w�肳�ꂽ {bgm} �̃f�[�^�����݂��܂���B" +
            $"AudioData �܂��� BgmEnum �̏��Ȃ��Ƃ�����ɓo�^����Ă��Ȃ��A���邢�́A�Đ�����BGM�̃f�[�^���w�肵�ԈႦ�Ă���\��������܂��B"));

        return -1;
    }

    /// <summary>
    /// SeClipData�ɓo�^���ꂽ�T�E���h���AEnum�̏��ɕ��בւ���
    /// </summary>
    /// <param name="data">SE�̓o�^�f�[�^</param>
    /// <param name="bgm">�T������SE��Enum</param>
    private int ConvertSeEnumIntoIndex(List<SeClipData> data, SeEnum se)
    {
        for (int i = 0; i < data.Count; i++)
        {
            if (se == data[i].type) return i;
        }

        Debug.LogError(string.Format($"�w�肳�ꂽ {se} �̃f�[�^�����݂��܂���B" +
            $"AudioData �܂��� BgmEnum �̏��Ȃ��Ƃ�����ɓo�^����Ă��Ȃ��A���邢�́A�Đ�����SE�̃f�[�^���w�肵�ԈႦ�Ă���\��������܂��B"));
        return -1;
    }

    /// <summary>
    /// �}�X�^�[�̉��ʂ�AudioMixer�ɃZ�b�g
    /// </summary>
    /// <param name="volume">����</param>
    public void SetMasterVolumeForAudioMixer(float volume)
    {
        //-80~0�ɕϊ�
        var convertedVolume = Mathf.Clamp(Mathf.Log10(volume) * 20f, -80f, 0f);
        //audioMixer�ɑ��
        _audioMixer.SetFloat("Master", convertedVolume);
        Debug.Log($"MasterVolume: {volume}");
    }

    /// <summary>
    /// BGM�̉��ʂ�AudioMixer�ɃZ�b�g
    /// </summary>
    /// <param name="volume">����</param>
    public void SetBgmVolumeForAudioMixer(float volume)
    {
        //-80~0�ɕϊ�
        var convertedVolume = Mathf.Clamp(Mathf.Log10(volume) * 20f, -80f, 0f);
        //audioMixer�ɑ��
        _audioMixer.SetFloat("BGM", convertedVolume);
        Debug.Log($"BGMVolume: {volume}");
    }

    /// <summary>
    /// SE�̉��ʂ�AudioMixer�ɃZ�b�g
    /// </summary>
    /// <param name="volume">����</param>
    public void SetSeVolumeForAudioMixer(float volume)
    {
        //-80~0�ɕϊ�
        var convertedVolume = Mathf.Clamp(Mathf.Log10(volume) * 20f, -80f, 0f);
        //audioMixer�ɑ��
        _audioMixer.SetFloat("SE", convertedVolume);
        Debug.Log($"SEVolume: {volume}");
    }

    /// <summary>
    /// �T�E���h�̐ݒ��ǂݍ���
    /// </summary>
    /// <returns>�ݒ�f�[�^</returns>
    private AudioData GetAudioData()
    {
        string path = "AudioData";
        AudioData data = Resources.Load<AudioData>(path);
        return data;
    }

    /// <summary>
    /// SE���Đ�����
    /// </summary>
    /// <param name="se">�Đ�����SE</param>
    public void PlaySE(SeEnum se)
    {
        int index = this.ConvertSeEnumIntoIndex(_audioData.seData, se);
        _seAudioSource.volume = _audioData.seData[index].volume;
        _seAudioSource.PlayOneShot(_audioData.seData[index].clip);
    }

    /// <summary>
    /// SE���Đ�����
    /// </summary>
    /// <param name="se">�Đ�����SE</param>
    /// <param name="isSingle">true���w�肵�ĒP���SE��炷�i�ȗ��\�j</param>
    public void PlaySE(SeEnum se, bool isSingle)
    {
        int index = this.ConvertSeEnumIntoIndex(_audioData.seData, se);
        _seAudioSource.clip = _audioData.seData[index].clip;
        _seAudioSource.volume = _audioData.seData[index].volume;
        if (isSingle) { _seAudioSource.Play(); } else { _seAudioSource.PlayOneShot(_audioData.seData[index].clip); }
    }

    public void StopSE() { _seAudioSource.Stop(); }
    public void PauseSE() { _seAudioSource.Pause(); }
    public void UnPauseSE() { _seAudioSource.UnPause(); }

    // �V�[�����ǂݍ��܂ꂽ�ۂɌĂ΂��
    private void PlayStackedBgm(Scene thisScene, Scene nextScene)
    {
        // �X�^�b�N����Ă���Ƃ��́A�V�[���J�ڌ��BGM��ω�
        if (_isStacked)
        {
            StopBGM();
            PlayBGM(stackedBgm);
            _isStacked = false;
        }
    }

    /// <summary>
    /// ���̃V�[�����ǂݍ��܂ꂽ�ۂɍĐ�����BGM��o�^����
    /// </summary>
    /// <param name="bgm">�o�^����BGM</param>
    public void StackBgm(BgmEnum bgm)
    {
        stackedBgm = bgm;
        _isStacked = true;
    }

    public void PlayBGM(BgmEnum bgm)
    {
        int index = this.ConvertBgmEnumIntoIndex(_audioData.bgmData, bgm);
        _bgmAudioSource.clip = _audioData.bgmData[index].clip;
        _bgmAudioSource.volume = _audioData.bgmData[index].volume;
        _bgmAudioSource.Play();
    }

    public void StopBGM() { _bgmAudioSource.Stop(); }
    public void PauseBGM() { _bgmAudioSource.Pause(); }
    public void UnPauseBGM() { _bgmAudioSource.UnPause(); }
}
