using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class BlinderPresenter : MonoBehaviour
{
    [SerializeField, Tooltip("ƒV[ƒ“‘JˆÚŽž‚Ì–Ú‰B‚µ")]
    private BlinderView _blinderView;
    [SerializeField, Tooltip("ƒV[ƒ“‘JˆÚ‚ðŠÇ—‚·‚éSceneTransitioner")]
    private SceneTransitioner _blinderModel;

    // Start is called before the first frame update
    void Start()
    {
        _blinderModel.HasTransitionedScene
            .Subscribe(x =>
            {
                _blinderView.SwitchScreenBlinder(x);
            })
            .AddTo(this);

        _blinderView.OnCompleteTransition
            .Subscribe(x =>
            {
                _blinderModel.onCompleteBlind = true;
            })
            .AddTo(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
