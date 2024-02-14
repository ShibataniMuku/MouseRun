using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioPresenter : MonoBehaviour
{
    [SerializeField, Tooltip("�T�E���h�𐧌�")]
    private AudioManager _audioModel;

    private Slider _masterSlider;
    private Slider _bgmSlider;
    private Slider _seSlider;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("kkkkkkkkkkkkkkk");

        SearchVolumeSlider();

        _masterSlider.OnValueChangedAsObservable()
            .Subscribe(x =>
            {
                _audioModel.SetMasterVolumeForAudioMixer(x);
            })
            .AddTo(this);

        _bgmSlider.OnValueChangedAsObservable()
            .Subscribe(x =>
            {
                _audioModel.SetBgmVolumeForAudioMixer(x);
            })
            .AddTo(this);

        _seSlider.OnValueChangedAsObservable()
            .Subscribe(x =>
            {
                _audioModel.SetSeVolumeForAudioMixer(x);
            })
            .AddTo(this);

        SceneManager.activeSceneChanged += ActiveSceneChanged;
    }

    private void ActiveSceneChanged(Scene thisScene, Scene nextScene)
    {
        SearchVolumeSlider();
    }

    /// <summary>
    /// �V�[�����ǂݍ��܂ꂽ�ۂɉ��ʒ����X���C�_�[��ݒ�
    /// </summary>
    private void SearchVolumeSlider()
    {
        _masterSlider = GameObject.Find("MasterSlider").GetComponent<Slider>();
        _bgmSlider = GameObject.Find("BGMSlider").GetComponent<Slider>();
        _seSlider = GameObject.Find("SESlider").GetComponent<Slider>();
    }
}
