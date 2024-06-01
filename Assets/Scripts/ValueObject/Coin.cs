using UnityEngine;

public class Coin : MonoBehaviour
{
    private readonly int _coin;
    public int Value { get { return _coin; } }

    private const int MIN = 0;

    public Coin(int coin)
    {
        if (coin >= MIN)
        {
            _coin = coin;
        }
        else
        {
            Debug.LogError("変数coinに0未満の値が設定されました。");
        }
    }

    public static Coin Sum(Coin coin1, Coin coin2)
    {
        return new Coin(coin1._coin + coin2._coin);
    }

    public bool CanDecrement(Coin coin)
    {
        return _coin - coin._coin >= 0;
    }

    public Coin Decrement(Coin coin)
    {
        if(_coin >= coin._coin)
        {
            return new Coin(_coin - coin._coin);
        }
        else
        {
            Debug.LogError("コインの枚数が負になりました。");
            return new Coin(0);
        }
    }

    public bool CompareGreaterThan(Coin coin)
    {
        return _coin > coin._coin;
    }
}
