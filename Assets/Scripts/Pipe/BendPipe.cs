using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BendPipe : Pipe
{
    // Update is called once per frame
    void Update()
    {
        
    }

    public override void InitializePipeDirections()
    {
        _directions[(int)TravelDirection.up] = point[0];
        _directions[(int)TravelDirection.right] = point[2];
    }
}
