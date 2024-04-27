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
            int posX = Random.Range(0, _pipeManager.gridCount.x);
            int posY = Random.Range(0, _pipeManager.gridCount.y);
            Grid grid = new Grid(posX, posY);
            GenerateItem(grid);
        }
    }

    public void GenerateItem(Grid grid)
    {
        // 生成する座標は、ItemManager類を管理するItemManagerで設定！！！！！！！！！！

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
        _itemManager.SetItem(new Grid(grid.x, grid.y));
    }

    public void PickUpItem(int posX, int posY, Score score)
    {
        _scoreManager.AddScore(score);
        // アイテム情報を削除
        _itemManager.RemoveItem(new Grid(posX, posY));


        // 次のアイテムを生成する処理 または アイテムが獲られて減ったことを伝える

    }
}
