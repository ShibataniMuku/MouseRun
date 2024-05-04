using TMPro;
using UnityEngine;

public class TimeView : MonoBehaviour
{
    [SerializeField, Header("残り時間を表示するテキスト")]
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
