public class ResultInfo
{
    public Score score;
    public Score levelBonus;
    public Score additionalBonus;
    public Coin gotCoin;
    public int gotExp;
    public Score highScore;
    public int ranking;

    public ResultInfo(Score score, Score levelBonus, Score additionalBonus, Coin gotCoin, int gotExp, Score highScore, int ranking)
    {
        this.score = score;
        this.levelBonus = levelBonus;
        this.additionalBonus = additionalBonus;
        this.gotCoin = gotCoin;
        this.gotExp = gotExp;
        this.highScore = highScore;
        this.ranking = ranking;
    }
}
