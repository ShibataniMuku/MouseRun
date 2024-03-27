using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Purchasing;
using UnityEngine;

public class TimeView : MonoBehaviour
{
    [SerializeField]
    GameObject _timeText;

    TextMeshProUGUI _timeTmp;

    // Start is called before the first frame update
    void Start()
    {
        _timeTmp = _timeText.GetComponent<TextMeshProUGUI>();
        _timeTmp.text = "00";
    }

    public void ResetTimeText(float time)
    {
        _timeTmp.text = time.ToString("f1");
    }
}
