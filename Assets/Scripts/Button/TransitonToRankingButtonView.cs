using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class TransitonToRankingButtonView : MonoBehaviour
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
