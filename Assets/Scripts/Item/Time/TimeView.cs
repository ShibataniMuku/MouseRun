using TMPro;
using UnityEngine;

public class TimeView : MonoBehaviour
{
    [SerializeField, Header("?c?????????????????????e?L?X?g")]
    private GameObject _timeText;

    TextMeshProUGUI _timeTmp;

    // Start is called before the first frame update
    void Awake()
    {
        _timeTmp = _timeText.GetComponent<TextMeshProUGUI>();
        _timeTmp.text = "00";
    }

    public void ResetTimeText(float time)
    {
        _timeTmp.text = time.ToString("f1");
    }
}
