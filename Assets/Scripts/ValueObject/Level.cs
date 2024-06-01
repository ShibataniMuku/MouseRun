using UnityEngine;

public class Level : MonoBehaviour
{
    private readonly int _level;
    public int Value { get { return _level; } }

    private const int MIN = 0;

    public Level(int level)
    {
        if (level >= MIN)
        {
            _level = level;
        }
        else
        {
            Debug.LogError("変数levelに0未満の値が設定されました。");
        }
    }

    public static Level Sum(Level level1, Level level2)
    {
        return new Level(level1._level + level2._level);
    }

    public bool CompareGreaterThan(Level level)
    {
        return _level > level._level;
    }
}
