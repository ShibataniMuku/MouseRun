using UnityEngine;
using Zenject;

public class TimeItemManager : MonoBehaviour, IItemManager
{
    //[Inject]
    private TimeManager _timeManager;
    [Inject]
    private ItemManager _itemManager;
    [Inject]
    private PipeManager _pipeManager;

    [SerializeField]
    GameObject _timeItem;
    [SerializeField]
    Transform _timeItemParent;

    public TimeItemManager(TimeManager timeManager, ItemManager itemManager)
    {
        _timeManager = timeManager;
        _itemManager = itemManager;

        InitFieldItem();
    }

    public void InitFieldItem()
    {
        // 時間延長のアイテムは、初期配置しない
    }

    public void GenerateItem(Grid grid)
    {
        // 生成する座標は、ItemManager類を管理するItemManagerで設定！！！！！！！！！！

        //アクティブでないオブジェクトをbulletsの中から探索
        foreach (Transform t in _timeItemParent)
        {
            if (!t.gameObject.activeSelf)
            {
                //非アクティブなオブジェクトの位置と回転を設定
                t.SetPositionAndRotation(_pipeManager.pipes[grid.x, grid.y].transform.position, Quaternion.identity);
                //アクティブにする
                t.gameObject.SetActive(true);
                return;
            }
        }

        //非アクティブなオブジェクトがない場合新規生成
        //生成時にbulletsの子オブジェクトにする
        Instantiate(_timeItem, _pipeManager.pipes[grid.x, grid.y].transform.position, Quaternion.identity, _timeItemParent);

        // アイテム情報を登録
        _itemManager.SetItemStatus(new Grid(grid.x, grid.y));
    }

    public void PickUpItem(int posX, int posY, float time)
    {
        _timeManager.MainTimer.AddTime(new TimeLimit(time));
        // アイテム情報を削除
        _itemManager.RemoveItem(new Grid(posX, posY));


        // 次のアイテムを生成する処理 または アイテムが獲られて減ったことを伝える

    }
}
