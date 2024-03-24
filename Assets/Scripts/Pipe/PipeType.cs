using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PipeType
{
    [SerializeField, Tooltip("メモは実行に無関係です")]
    private string _memo;
    [Space(5)]

    [Tooltip("パイプの種類")]
    public PipeEnum type;
    [Tooltip("パイプオブジェクト")]
    public GameObject pipeObj;
    [Tooltip("フィールド内部の出現確率"), Range(0, 100)]
    public float inProbability = 50;
    [Tooltip("フィールド外周の出現確率"), Range(0, 100)]
    public float outProbability = 50;
    [Tooltip("フィールドの角の出現確率"), Range(0, 100)]
    public float edgeProbability = 50;
}

public enum PipeEnum
{
    bend,
    straight
}