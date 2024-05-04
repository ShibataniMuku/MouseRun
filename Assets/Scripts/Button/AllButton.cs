using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(EventTrigger))]
public class AllButton : MonoBehaviour
{
    [SerializeField, Tooltip("ボタン本体")]
    protected Button _button;

    protected float _defaultScale;

    public virtual void Start()
    {
    
    }

    /// <summary>
    /// ボタンが押されたときに呼ばれる
    /// </summary>
    protected virtual void OnClicked()
    {

    }
}
