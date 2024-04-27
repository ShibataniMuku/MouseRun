using Zenject;

public class ItemManager
{
    private PipeManager _pipeManager;
    public bool[,] Items { get; private set; }

    ItemManager(PipeManager pipeManager)
    {
        _pipeManager = pipeManager;
        Items = new bool[_pipeManager.gridCount.x, _pipeManager.gridCount.y];
    }

    public void SetItem(Grid grid)
    {
        Items[grid.x, grid.y] = true;
    }

    public void RemoveItem(Grid grid)
    {
        Items[grid.x, grid.y] = false;
    }
}
