using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class PipeManager : MonoBehaviour
{
    [SerializeField, Header("パイプのマス数")]
    public Vector2Int gridCount = new Vector2Int(6, 6);

    [SerializeField]
    private PipeType[] pipeType = new PipeType[2];
    [SerializeField]
    RectTransform fieldArea; // パイプが表示されうるエリア
    [SerializeField]
    Transform pipeParent; // パイプの親オブジェクト

    GameObject[,] pipeObjects;
    
    public Pipe[,] pipes;

    [Inject]
    private PlayingPhase _playingPhase;

    private void Awake()
    {
        pipeObjects = new GameObject[gridCount.x, gridCount.y];
        pipes = new Pipe[gridCount.x, gridCount.y];
        GeneratePipes(gridCount);
    }

    // Start is called before the first frame update
    void Start()
    {
        // 操作可能フェーズになったら、回転可能にする
        _playingPhase.SetOnStartGame(() =>
        {
            foreach (Pipe pipe in pipes) pipe.SetCanRotational(true);
            return UniTask.CompletedTask;
        });

        // 操作不能フェーズになったら、回転不能にする
        _playingPhase.SetOnFinishGame(() =>
        {
            foreach (Pipe pipe in pipes) pipe.SetCanRotational(false);
            return UniTask.CompletedTask;
        });
    }

    /// <summary>
    /// フィールドを生成する
    /// </summary>
    /// <param name="fieldGrid">フィールドのサイズ</param>
    private void GeneratePipes(Vector2Int gridCount)
    {
        //float fieldSizeX = Screen.width - fieldArea.offsetMin.x - fieldArea.offsetMax.x;
        //float fieldSizeY = Screen.height - fieldArea.offsetMin.y - fieldArea.offsetMax.y;
        //Vector2 fieldSize = new Vector2(fieldSizeX, fieldSizeY ); // パイプが表示されうるエリアの大きさ
        //float gridSize = 0; // 1マス分の大きさ
        //Vector2 margin; // 画面縦横比による余白

        Vector3[] corners = new Vector3[4]; // RectTransformの左下のワールド座標を取得
        fieldArea.GetWorldCorners(corners);


        float fieldSizeX = corners[2].x - corners[0].x;
        float fieldSizeY = corners[1].y - corners[0].y;
        Vector2 fieldSize = new Vector2(fieldSizeX, fieldSizeY); // パイプが表示されうるエリアの大きさ
        float gridSize = 0; // 1マス分の大きさ
        Vector2 margin; // 画面縦横比による余白




        // 縦横の長さによって、横に余白を作るか、縦に余白を作るかを決定
        if ((fieldSize.x) / gridCount.x <= (fieldSize.y) / gridCount.y)
        {
            gridSize = fieldSize.x / gridCount.x;
            Debug.Log("x軸方向の方が短い");
        }
        else
        {
            gridSize = fieldSize.y / gridCount.y;
        }

        // パイプの配置を横幅に合わせる
        margin.x = fieldSize.x - gridSize * gridCount.x;
        margin.y = fieldSize.y - gridSize * gridCount.y;

        // パイプを生成
        for (int iy = 0; iy < gridCount.y; iy++)
        {
            for (int jx = 0; jx < gridCount.x; jx++)
            {
                // 余白 + 半マス + 1マスの高さ × マス数
                float screenPosX = margin.x / 2 + gridSize / 2 + gridSize * jx;
                // SafeArea + 余白 + 半マス + 1マスの高さ × マス数
                float screenPosY = corners[0].y + margin.y / 2 + gridSize / 2 + gridSize * iy;

                Vector3 screenPos = new Vector3(screenPosX, screenPosY, 0);
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos) - new Vector3(0, 0, Camera.main.transform.position.z);

                pipeObjects[jx, iy] = Instantiate(DecidePipeType(jx, iy), pipeParent);
                Transform t = pipeObjects[jx, iy].transform;
                t.position = worldPos;
                t.localScale = Vector3.one * gridSize / 150;
                pipes[jx, iy] = pipeObjects[jx, iy].GetComponent<Pipe>();
                pipes[jx, iy].InitializePipeDirections();
            }
        }
    }

    /// <summary>
    /// パイプの種類を決定する
    /// </summary>
    /// <param name="x">xマス座標</param>
    /// <param name="y">yマス座標</param>
    /// <returns></returns>
    private GameObject DecidePipeType(int x, int y)
    {
        int num = Random.Range(0, 100);

        if ((x == 0 && (y == 0 || y == gridCount.y - 1)) || (x == gridCount.x - 1 && (y == 0 || y == gridCount.y - 1)))
        {
            // 四つ角
            if (num <= (pipeType[0].edgeProbability / (pipeType[0].edgeProbability + pipeType[1].edgeProbability)) * 100)
            {
                return pipeType[0].pipeObj;
            }
            else
            {
                return pipeType[1].pipeObj;
            }
        }
        else if (x == gridCount.x - 1 || x == 0 || y == gridCount.y - 1 || y == 0)
        {
            // 外周
            if (num <= (pipeType[0].outProbability / (pipeType[0].outProbability + pipeType[1].outProbability)) * 100)
            {
                return pipeType[0].pipeObj;
            }
            else
            {
                return pipeType[1].pipeObj;
            }
        }
        else
        {
            // 内部
            if (num <= (pipeType[0].inProbability / (pipeType[0].inProbability + pipeType[1].inProbability)) * 100)
            {
                return pipeType[0].pipeObj;
            }
            else
            {
                return pipeType[1].pipeObj;
            }
        }
    }
}