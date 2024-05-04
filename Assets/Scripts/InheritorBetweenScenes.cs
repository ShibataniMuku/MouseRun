using System;
using System.Collections.Generic;

public class InheritorBetweenScenes
{
    private Dictionary<String, int> inheritedData = new Dictionary<String, int>();

    private InheritorBetweenScenes()
    {

    }

    public void SetInheritedData(String key, int value)
    {
        inheritedData.Add(key, value);
    }

    public int GetInheritedData(String key)
    {
        return inheritedData[key];
    }

    public void ClearAllInheritedData()
    {
        inheritedData.Clear();
    }
}
