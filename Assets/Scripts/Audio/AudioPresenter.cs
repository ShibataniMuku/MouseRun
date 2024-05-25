using UniRx;
using Zenject;

public class AudioPresenter
{
    private AudioManager _audioModel;
    [Inject]
    private AudioView _audioView;

    private CompositeDisposable _compositeDisposable;

    public AudioPresenter(AudioManager audioManager)
    {
        _audioModel = audioManager;

        _compositeDisposable = new CompositeDisposable();

        _audioView.MasterSlider.OnValueChangedAsObservable()
            .Subscribe(x =>
            {
                _audioModel.SetMasterVolumeForAudioMixer(x);
            })
            .AddTo(_compositeDisposable);

        _audioView.BgmSlider.OnValueChangedAsObservable()
            .Subscribe(x =>
            {
                _audioModel.SetBgmVolumeForAudioMixer(x);
            })
            .AddTo(_compositeDisposable);

        _audioView.SeSlider.OnValueChangedAsObservable()
            .Subscribe(x =>
            {
                _audioModel.SetSeVolumeForAudioMixer(x);
            })
            .AddTo(_compositeDisposable);
    }

    // Start is called before the first frame update
    void Start()
    {
        //SearchVolumeSlider();



        //SceneManager.activeSceneChanged += ActiveSceneChanged;
    }

    //private void ActiveSceneChanged(Scene thisScene, Scene nextScene)
    //{
    //    SearchVolumeSlider();
    //}
}
