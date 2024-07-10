using UnityEngine;

public class FieldPosition
{
    public Grid grid;
    public Vector3 pos;

    public FieldPosition(Grid grid, Vector3 pos)
    {
        this.grid = grid;
        this.pos = pos;
    } 
}
