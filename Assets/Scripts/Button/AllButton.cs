using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(EventTrigger))]
public class AllButton : MonoBehaviour
{
    [SerializeField, Tooltip("�{�^���{��")]
    protected Button _button;

    protected float _defaultScale;

    public virtual void Start()
    {
    
    }

    /// <summary>
    /// �{�^���������ꂽ�Ƃ��ɌĂ΂��
    /// </summary>
    protected virtual void OnClicked()
    {

    }
}
