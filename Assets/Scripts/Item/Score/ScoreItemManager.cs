using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class ScoreItemManager : MonoBehaviour
{
    [Header("生成されるまでの時間")]
    [SerializeField]
    float _separateGeneratingItem = 3;
    [Header("初期の配置数")]
    private int _defaultSettingCount = 8;

    [SerializeField]
    GameObject _scoreItem;
    [SerializeField]
    Transform _ScoreItemParent;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < _defaultSettingCount; i++)
        {
            GenerateScoreItem();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WaitGeneratingScoreItem()
    {
        Invoke("GenerateScoreItem", _separateGeneratingItem);
    }

    public void GenerateScoreItem()
    {
        int posX = Random.Range(0, PipeManager.pipes.GetLength(0));
        int posY = Random.Range(0, PipeManager.pipes.GetLength(1));

        //アクティブでないオブジェクトをbulletsの中から探索
        foreach (Transform t in _ScoreItemParent)
        {
            if (!t.gameObject.activeSelf)
            {
                //非アクティブなオブジェクトの位置と回転を設定
                t.SetPositionAndRotation(PipeManager.pipes[posX, posY].transform.position, Quaternion.identity);
                //アクティブにする
                t.gameObject.SetActive(true);
                return;
            }
        }

        //非アクティブなオブジェクトがない場合新規生成
        //生成時にbulletsの子オブジェクトにする
        Instantiate(_scoreItem, PipeManager.pipes[posX, posY].transform.position, Quaternion.identity, _ScoreItemParent);
    }
}
