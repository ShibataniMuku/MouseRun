<<<<<<< Updated upstream
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
=======
ï»¿using UnityEngine;
using System;
>>>>>>> Stashed changes

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
            Debug.LogError("•Ï”Score‚É0–¢–ž‚Ì’l‚ªÝ’è‚³‚ê‚Ü‚µ‚½B");
        }
    }

    public static Score Sum(Score score1, Score score2)
    {
        return new Score(score1._score + score2._score);
    }

    public bool CompareGreaterThan(Score score)
    {
        return _score > score._score;
    }

    public Score ConvertLevelBonus(Level level)
    {
        Score levelBonus = new Score((int)(_score * 0.1f * Math.Sqrt(level.Value)));
        return levelBonus;
    }
}
