using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System;
using UnityEngine;
using Zenject;

public class ScoreItemManager : MonoBehaviour, IItemManager, IInitializable
{
    [Inject]
    private ScoreManager _scoreManager;
    [Inject]
    private ItemManager _itemManager;
    [Inject]
    private PipeManager _pipeManager;

    [SerializeField, Header("生成されるまでの時間")]
    private float _separateGeneratingItem = 3;
    [SerializeField, Header("初期の配置数")]
    private int _defaultSettingCount = 8;

    [SerializeField]
    GameObject _scoreItem;
    [SerializeField]
    Transform _scoreItemParent;

    public void Initialize()
    {
        Debug.Log("スコアアイテムの配置を初期化しました");

        InitFieldItem();
    }

    public void InitFieldItem()
    {
        // アイテムの配置の初期化
        for(int i = 0; i < _defaultSettingCount; i++)
        {
            List<Grid> isNotPlaced = _itemManager.GetItemStatusList();

            int grid =  UnityEngine.Random.Range(0, isNotPlaced.Count);
            GenerateItem(isNotPlaced[grid]);
        }
    }

    public void GenerateItem(Grid grid)
    {
        if (_itemManager.GetItemStatus(grid))
        {
            Debug.LogError("既にアイテムが配置されている足場に、重複してアイテムを配置しようとしています。");
            return;
        }

        //アクティブでないオブジェクトをbulletsの中から探索
        foreach (Transform t in _scoreItemParent)
        {
            if (!t.gameObject.activeSelf)
            {
                //非アクティブなオブジェクトの位置と回転を設定
                t.SetPositionAndRotation(_pipeManager.pipes[grid.x, grid.y].transform.position, Quaternion.identity);
                //アクティブにする
                t.gameObject.SetActive(true);

                t.gameObject.GetComponent<IPickUpable>().InitPosition(grid);
                return;
            }
        }

        //非アクティブなオブジェクトがない場合新規生成
        //生成時にbulletsの子オブジェクトにする
        GameObject item = Instantiate(_scoreItem, _pipeManager.pipes[grid.x, grid.y].transform.position, Quaternion.identity, _scoreItemParent);
        item.GetComponent<IPickUpable>().InitPosition(grid);
        item.GetComponent<ScoreItem>().Init(this);

        // アイテム情報を登録
        _itemManager.SetItemStatus(new Grid(grid.x, grid.y));
    }

    /// <summary>
    /// アイテムをランダムに生成する
    /// </summary>
    /// <param name="delayTime">何秒後に生成するか</param>
    public async void GenerateItem(float delayTime)
    {
        float time = UnityEngine.Random.Range(delayTime - 3, delayTime + 3);

        List<Grid> isNotPlaced = _itemManager.GetItemStatusList();
        int grid = UnityEngine.Random.Range(0, isNotPlaced.Count);

        await UniTask.Delay(TimeSpan.FromSeconds(time));

        GenerateItem(isNotPlaced[grid]);
    }

    public void PickUpItem(int posX, int posY, Score score)
    {
        _scoreManager.AddScore(score);
        // アイテム情報を削除
        _itemManager.RemoveItem(new Grid(posX, posY));

        GenerateItem(_separateGeneratingItem);
    }
}
