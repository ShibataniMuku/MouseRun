using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PipeType
{
    [SerializeField, Tooltip("�����͎��s�ɖ��֌W�ł�")]
    private string _memo;
    [Space(5)]

    [Tooltip("�p�C�v�̎��")]
    public PipeEnum type;
    [Tooltip("�p�C�v�I�u�W�F�N�g")]
    public GameObject pipeObj;
    [Tooltip("�t�B�[���h�����̏o���m��"), Range(0, 100)]
    public float inProbability = 50;
    [Tooltip("�t�B�[���h�O���̏o���m��"), Range(0, 100)]
    public float outProbability = 50;
    [Tooltip("�t�B�[���h�̊p�̏o���m��"), Range(0, 100)]
    public float edgeProbability = 50;
}

public enum PipeEnum
{
    bend,
    straight
}