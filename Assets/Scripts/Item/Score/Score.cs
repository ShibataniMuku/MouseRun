using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Score
{
    private readonly int _score;
    public int Value { get { return _score; } }

    private const int MIN = 0;

    public Score(int score)
    {
        if(score >= MIN)
        {
            _score = score;
        }
        else
        {
            Debug.LogError("•Ï”Score‚É0–¢–‚Ì’l‚ªİ’è‚³‚ê‚Ü‚µ‚½B");
        }
    }

    public static Score Sum(Score score1, Score score2)
    {
        return new Score(score1.Value + score2.Value);
    }
}
