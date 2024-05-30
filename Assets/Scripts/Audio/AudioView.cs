using UnityEngine;
using UnityEngine.UI;

public class AudioView : MonoBehaviour
{
    [SerializeField]
    private Slider _masterSlider;
    [SerializeField]
    private Slider _bgmSlider;
    [SerializeField]
    private Slider _seSlider;

    public Slider MasterSlider => _masterSlider;
    public Slider BgmSlider => _bgmSlider;
    public Slider SeSlider => _seSlider;
}
