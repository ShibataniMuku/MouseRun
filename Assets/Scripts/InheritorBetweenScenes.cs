using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class InheritorBetweenScenes : MonoBehaviour
{
    public static InheritorBetweenScenes inheritorBetweenScenesInstance = null;

    private Dictionary<String, int> inheritedData = new Dictionary<String, int>();

    private void Awake()
    {
        if (inheritorBetweenScenesInstance == null)
        {
            inheritorBetweenScenesInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
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
