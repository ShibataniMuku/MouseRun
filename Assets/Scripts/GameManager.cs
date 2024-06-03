using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool SetHighScore(Score score)
    {
        // 現時点でのハイスコアを取得
        int currentHighScore;

        if(score.CompareGreaterThan(currentHighScore))
        {
            // ハイスコアを更新
            return true;
        }
        else
        {
            return false;
        }
    }

    public Score GetHighScore()
    {

    }

    public void AddCoin(Coin coin)
    {

    }

    public bool DecrementCoin(Coin coin)
    {
        // 現時点での所有コインを取得
        Coin currentCoin;

        if(currentCoin.CompareGreaterThan(coin))
        {
            // 所有コインを減らす処理
            return true;
        }
        else
        {
            return false;
        }
    }

    public Coin GetCurrentCoin()
    {
        Coin currentCoin;

        // コインを取得する処理

        return currentCoin;
    }

    public void AddLevel(Level level)
    {
        
    }

    public void GetLevel()
    {
        Level level;

        // レベルを取得する処理

        return level;
    }
}
