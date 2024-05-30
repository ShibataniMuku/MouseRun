public class ResultInfo
{
    public Score score;
    public int levelBonus;
    public int additionalBonus;
    public int gotCoin;
    public int gotExp;
    public Score highScore;
    public int ranking;

    public ResultInfo(Score score, int levelBonus, int additionalBonus, int gotCoin, int gotExp, Score highScore, int ranking)
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
