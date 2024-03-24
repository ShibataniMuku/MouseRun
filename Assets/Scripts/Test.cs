using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteAlways]
public class Test : MonoBehaviour
{
    public RectTransform button;
    public RectTransform dialogue;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("button" + button.position);
        Debug.Log("dialogue " + dialogue.position);

        dialogue.position = button.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnValidate()
    {
        /*
        Debug.Log(Screen.width);
        Debug.Log($"position : {Camera.main.WorldToScreenPoint(Camera.main.ScreenToWorldPoint(rt.position))}");
        Debug.Log("sizeDelta.x : " + rt.sizeDelta.x);
        Debug.Log("sizeDelta.y : " + rt.sizeDelta.y);
        Debug.Log("=================================");
        */
        /*
        Debug.Log($"button : {button.position}");
        Debug.Log($"dialogue : {dialogue.position}");
        Debug.Log("=======================================");
        */
    }
}
