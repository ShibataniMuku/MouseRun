using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLimit
{
    public float _time;
    public float Value { get { return _time; } }

    private const float MAX = 99;
    private const float MIN = 0;

    public TimeLimit(float time)
    {
        if (time >= MIN)
        {
            _time = Mathf.Min(time, MAX);
        }
        else
        {
            Debug.LogError("�ϐ�time�ɕs�K�؂Ȓl���ݒ肳��܂����B");
        }
    }
}
