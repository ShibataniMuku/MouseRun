using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioData", menuName = "ScriptableObjects/CreateAudioParamAsset")]
public class AudioData : ScriptableObject
{
    public List<BgmClipData> bgmData = new List<BgmClipData>();
    public List<SeClipData> seData = new List<SeClipData>();
}

[System.Serializable]
public class BgmClipData
{
    [SerializeField, Tooltip("メモは実行に無関係です")]
    private string _memo;
    [Space(5)]

    [Tooltip("サウンドの種別")]
    public BgmEnum type;
    [Tooltip("サウンドファイル")]
    public AudioClip clip;
    [Tooltip("音量"), Range(0, 1)]
    public float volume = 0.5f;
}

[System.Serializable]
public class SeClipData
{
    [SerializeField, Tooltip("メモは実行に無関係です")]
    private string _memo;
    [Space(5)]

    [Tooltip("サウンドの種別")]
    public SeEnum type;
    [Tooltip("サウンドファイル")]
    public AudioClip clip;
    [Tooltip("音量"), Range(0, 1)]
    public float volume = 0.5f;
}

public enum BgmEnum
{
    title,
    play,
    result
}

public enum SeEnum
{
    pressedButton,
    hoveredButton
}
