public class GameManager
{
    private Level level;
    private Coin coin;
    private Score highScore;

    public GameManager()
    {

    }

    public bool UpdateHighScore(Score earnedScore)
    {
        if(earnedScore.CompareGreaterThan(highScore))
        {
            highScore = earnedScore;

            // EasySaveの値も更新

            return true;
        }
        else
        {
            return false;
        }
    }

    public Score GetHighScore()
    {
        Score highScore = new Score(0);

        // EasySaveから値を取得

        return highScore;
    }

    public void IncrementCoin(Coin migrationCoin)
    {
        coin = Coin.Sum(coin, migrationCoin);

        // EasySaveの値も更新
    }

    public bool DecrementCoin(Coin migrationCoin)
    {
        if (coin.CanDecrement(migrationCoin))
        {
            coin = coin.Decrement(migrationCoin);
            // EasySaveの値も更新
            return true;
        }
        else
        {
            return false;
        }
    }

    public Level GetLevel()
    {
        Level lv = new Level(0);

        // EasySaveから値を取得

        return lv;
    }

    public void IncrementLevel(Level lv)
    {
        level = Level.Sum(level, lv);

        // EasySaveの値も更新
    }
}
