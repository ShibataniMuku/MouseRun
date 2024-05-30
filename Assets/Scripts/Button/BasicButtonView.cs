using UniRx;
using UnityEngine.UI;
using UnityEngine;

public class BasicButtonView : MonoBehaviour
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
