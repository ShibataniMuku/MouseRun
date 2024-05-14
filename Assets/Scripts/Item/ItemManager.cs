using System.Collections.Generic;

public class ItemManager
{
    private PipeManager _pipeManager;
    private bool[,] Items;

    ItemManager(PipeManager pipeManager)
    {
        _pipeManager = pipeManager;
        Items = new bool[_pipeManager.gridCount.x, _pipeManager.gridCount.y];
    }

    public void SetItemStatus(Grid grid)
    {
        Items[grid.x, grid.y] = true;
    }

    public bool GetItemStatus(Grid grid)
    {
        return Items[grid.x, grid.y];
    }

    /// <summary>
    /// 何も配置されていない座標のリストを返す
    /// </summary>
    /// <returns></returns>
    public List<Grid> GetItemStatusList()
    {
        List<Grid> placedPos = new List<Grid>();

        for(int i = 0; i < Items.GetLength(0); i++)
        {
            for (int j = 0; j < Items.GetLength(1); j++)
            {
                if (!Items[i, j]) placedPos.Add(new Grid(i, j));
            }
        }

        return placedPos;
    }

    public void RemoveItem(Grid grid)
    {
        Items[grid.x, grid.y] = false;
    }
}
