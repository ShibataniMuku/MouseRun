[System.Serializable]
public class Grid
{
    private readonly int _x = 0;
    private readonly int _y = 0;

    public int x => _x;
    public int y => _y;

    public Grid(int x, int y)
    {
        _x = x;
        _y = y;
    }

    public Grid GetNextGrid(int x, int y)
    {
        return new Grid(_x + x, _y + y);
    }
}
