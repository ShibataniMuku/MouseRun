using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [SerializeField, Tooltip("オーディオミキサー")]
    private AudioMixer _audioMixer;
    [SerializeField, Tooltip("BGM用のAudioSource")]
    private AudioSource _bgmAudioSource;
    [SerializeField, Tooltip("SE用のAudioSource")]
    private AudioSource _seAudioSource;

    private AudioData _audioData;
    private BgmEnum stackedBgm;
    private bool _isStacked = false; // BGMがスタックされているか否か

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

        // ================ ↓ とりあえずBGMをテストで流している ==============================
       PlayBGM(BgmEnum.title);
    }

    /// <summary>
    /// 同一のBGMを登録していないかを検査
    /// </summary>
    /// <param name="data">登録BGMデータ</param>
    private void CheckBgmOverlap(List<BgmClipData> data)
    {
        List<BgmEnum> bgm = new List<BgmEnum>();
        for (int i = 0; i < data.Count; i++)
        {
            if (bgm.Contains(data[i].type))
            {
                Debug.LogError(string.Format($"サウンド {data[i].type} が重複しています"));
            }
            else
            {
                bgm.Add(data[i].type);
            }
        }
    }

    /// <summary>
    /// 同一のSEを登録していないかを検査
    /// </summary>
    /// <param name="data">登録SEデータ</param>
    private void CheckSeOverlap(List<SeClipData> data)
    {
        List<SeEnum> bgm = new List<SeEnum>();
        for (int i = 0; i < data.Count; i++)
        {
            if (bgm.Contains(data[i].type))
            {
                Debug.LogError(string.Format($"サウンド {data[i].type} が重複しています"));
            }
            else
            {
                bgm.Add(data[i].type);
            }
        }
    }

    /// <summary>
    /// BgmClipDataに登録されたサウンドを、Enumの順に並べ替える
    /// </summary>
    /// <param name="data">BGMの登録データ</param>
    /// <param name="bgm">探したいBGMのEnum</param>
    private int ConvertBgmEnumIntoIndex(List<BgmClipData> data, BgmEnum bgm)
    {
        for (int i = 0; i < data.Count; i++)
        {
            if (bgm == data[i].type) return i;
        }

        Debug.LogError(string.Format($"指定された {bgm} のデータが存在しません。" +
            $"AudioData または BgmEnum の少なくとも一方に登録されていない、あるいは、再生するBGMのデータを指定し間違えている可能性があります。"));

        return -1;
    }

    /// <summary>
    /// SeClipDataに登録されたサウンドを、Enumの順に並べ替える
    /// </summary>
    /// <param name="data">SEの登録データ</param>
    /// <param name="bgm">探したいSEのEnum</param>
    private int ConvertSeEnumIntoIndex(List<SeClipData> data, SeEnum se)
    {
        for (int i = 0; i < data.Count; i++)
        {
            if (se == data[i].type) return i;
        }

        Debug.LogError(string.Format($"指定された {se} のデータが存在しません。" +
            $"AudioData または BgmEnum の少なくとも一方に登録されていない、あるいは、再生するSEのデータを指定し間違えている可能性があります。"));
        return -1;
    }

    /// <summary>
    /// マスターの音量をAudioMixerにセット
    /// </summary>
    /// <param name="volume">音量</param>
    public void SetMasterVolumeForAudioMixer(float volume)
    {
        //-80~0に変換
        var convertedVolume = Mathf.Clamp(Mathf.Log10(volume) * 20f, -80f, 0f);
        //audioMixerに代入
        _audioMixer.SetFloat("Master", convertedVolume);
        Debug.Log($"MasterVolume: {volume}");
    }

    /// <summary>
    /// BGMの音量をAudioMixerにセット
    /// </summary>
    /// <param name="volume">音量</param>
    public void SetBgmVolumeForAudioMixer(float volume)
    {
        //-80~0に変換
        var convertedVolume = Mathf.Clamp(Mathf.Log10(volume) * 20f, -80f, 0f);
        //audioMixerに代入
        _audioMixer.SetFloat("BGM", convertedVolume);
        Debug.Log($"BGMVolume: {volume}");
    }

    /// <summary>
    /// SEの音量をAudioMixerにセット
    /// </summary>
    /// <param name="volume">音量</param>
    public void SetSeVolumeForAudioMixer(float volume)
    {
        //-80~0に変換
        var convertedVolume = Mathf.Clamp(Mathf.Log10(volume) * 20f, -80f, 0f);
        //audioMixerに代入
        _audioMixer.SetFloat("SE", convertedVolume);
        Debug.Log($"SEVolume: {volume}");
    }

    /// <summary>
    /// サウンドの設定を読み込む
    /// </summary>
    /// <returns>設定データ</returns>
    private AudioData GetAudioData()
    {
        string path = "AudioData";
        AudioData data = Resources.Load<AudioData>(path);
        return data;
    }

    /// <summary>
    /// SEを再生する
    /// </summary>
    /// <param name="se">再生するSE</param>
    public void PlaySE(SeEnum se)
    {
        int index = this.ConvertSeEnumIntoIndex(_audioData.seData, se);
        _seAudioSource.volume = _audioData.seData[index].volume;
        _seAudioSource.PlayOneShot(_audioData.seData[index].clip);
    }

    /// <summary>
    /// SEを再生する
    /// </summary>
    /// <param name="se">再生するSE</param>
    /// <param name="isSingle">trueを指定して単一のSEを鳴らす（省略可能）</param>
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

    // シーンが読み込まれた際に呼ばれる
    private void PlayStackedBgm(Scene thisScene, Scene nextScene)
    {
        // スタックされているときは、シーン遷移後にBGMを変化
        if (_isStacked)
        {
            StopBGM();
            PlayBGM(stackedBgm);
            _isStacked = false;
        }
    }

    /// <summary>
    /// 次のシーンが読み込まれた際に再生するBGMを登録する
    /// </summary>
    /// <param name="bgm">登録するBGM</param>
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
