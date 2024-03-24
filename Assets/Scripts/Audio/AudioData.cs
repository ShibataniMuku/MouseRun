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
    [SerializeField, Tooltip("�����͎��s�ɖ��֌W�ł�")]
    private string _memo;
    [Space(5)]

    [Tooltip("�T�E���h�̎��")]
    public BgmEnum type;
    [Tooltip("�T�E���h�t�@�C��")]
    public AudioClip clip;
    [Tooltip("����"), Range(0, 1)]
    public float volume = 0.5f;
}

[System.Serializable]
public class SeClipData
{
    [SerializeField, Tooltip("�����͎��s�ɖ��֌W�ł�")]
    private string _memo;
    [Space(5)]

    [Tooltip("�T�E���h�̎��")]
    public SeEnum type;
    [Tooltip("�T�E���h�t�@�C��")]
    public AudioClip clip;
    [Tooltip("����"), Range(0, 1)]
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
