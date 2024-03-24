using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class ScoreItem : MonoBehaviour, IPickUpable
{
    [SerializeField]
    int _addedScore = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Mouse"))
        {
            collision.gameObject.GetComponent<Characters>().AddScore(_addedScore);
            gameObject.SetActive(false);
        }
    }

    public void GetItem()
    {

        // �����ɁA���_�����Z���鏈�����L�q

        gameObject.SetActive(false);
    }
}
