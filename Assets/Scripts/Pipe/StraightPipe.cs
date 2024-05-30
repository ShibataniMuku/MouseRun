using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightPipe : Pipe
{
    // Update is called once per frame
    void Update()
    {
        
    }

    public override void InitializePipeDirections()
    {
        _directions[(int)TravelDirection.up] = point[0];
        _directions[(int)TravelDirection.down] = point[2];
        base.InitializePipeDirections();
    }
}
