using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreItem : MonoBehaviour, IPickUpable
{
    [SerializeField]
    private int _addScore = 1;

    public void GetItem()
    {
        // “¾“_‚ð‰ÁŽZ
        ScoreManager.scoreManagerInstance.AddScore(new Score(_addScore));

        gameObject.SetActive(false);
    }
}
