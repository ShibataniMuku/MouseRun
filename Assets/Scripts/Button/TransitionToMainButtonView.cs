using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class TransitionToMainButtonView : MonoBehaviour
{
    [SerializeField]
    private GameObject button;

    public Subject<Unit> buttonSubject = new Subject<Unit>();

    // Start is called before the first frame update
    void Start()
    {
        button.GetComponent<Button>().OnClickAsObservable()
            .Subscribe(x =>
            {
                buttonSubject.OnNext(Unit.Default);
            });
    }
}
