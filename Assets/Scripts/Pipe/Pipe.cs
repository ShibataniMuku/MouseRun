using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Pipe : MonoBehaviour, IPointerClickHandler
{
    [SerializeField, Header("回転方向")]
    private Way way;
    [SerializeField, Header("回転方向")]
    private float rotationAnimDuration = 0.4f;

    [SerializeField]
    protected Transform[] point = new Transform[3];
    [SerializeField]
    private GameObject pipe;

    protected Transform[] _directions = new Transform[4]; // パイプの方向（0:上、1:右、2:下、3:左）
    private bool isRotational = false; // 現在回転しているか否か
    private bool isOnCharacter = false; // 現在パイプの上にキャラがいるが否か
    private bool canRotational = false; // 回転させることができるか否か

    private void Awake()
    {

    }

    // Start is called before the first frame update
    protected void Start()
    {
        RotatePipe(way, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 次にこのパイプに渡ることができるか否かを調査し、可能であればパスにポイントを追加
    /// </summary>
    /// <param name="path">移動経路のパス</param>
    /// <param name="travelDirection">このパイプに侵入してくる方向</param>
    public bool ResetPathPoint(List<Transform> path, TravelDirection travelDirection)
    {
        // 道が繋がっている、かつ回転中でなければ経路を更新
        if (_directions[(int)travelDirection] != null && !isRotational)
        {
            path.Clear();
            path.Add(_directions[(int)travelDirection]);
            path.Add(point[1]);
            if (_directions[(int)travelDirection] == point[0]) { path.Add(point[2]); } else { path.Add(point[0]); }
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 片方から侵入した際に、もう片方はどの方向かを返す
    /// </summary>
    /// <param name="pos">マス座標</param>
    /// <param name="travelDirection">入口の方向</param>
    public TravelDirection GetExitPoint(Vector2Int pos, TravelDirection enterDirection)
    {
        TravelDirection exit = (TravelDirection)(-1);

        for(int i = 0; i < _directions.Length; i++)
        {
            if (_directions[i] != null && i != (int)enterDirection)
            {
                exit = (TravelDirection)i;
            }
        }

        return exit;
    }

    /// <summary>
    /// パイプがクリックされた際に呼ばれる
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (isRotational)
        {
            Debug.Log("回転中は回転できません");
            return;
        }
        if (isOnCharacter)
        {
            Debug.Log("キャラクターが乗っているときは回転できません");
            return;
        }
        if (!canRotational)
        {
            Debug.Log("現在、回転させることができません");
            return;
        }

        RotatePipe(way, false);
    }

    /// <summary>
    /// キャラが乗っているか否かを設定
    /// </summary>
    /// <param name="isOn">true:乗っている, false;乗っていない</param>
    public void SetIsOnCharacter(bool isOn)
    {
        isOnCharacter = isOn;
    }

    /// <summary>
    /// パイプを回転させる
    /// </summary>
    /// <param name="way">回転の方向</param>
    /// <param name="isRandom">ランダムに回転させるか否か（trueの場合、アニメーションなし）</param>
    public void RotatePipe(Way way, bool isRandom)
    {
        int loopCount = 1;
        if (isRandom) loopCount = Random.Range(1, 5);

        for (int i = 0; i < loopCount; i++)
        {
            if (way == Way.clockwise)
            {
                Transform tmp = _directions[3];
                _directions[3] = _directions[2];
                _directions[2] = _directions[1];
                _directions[1] = _directions[0];
                _directions[0] = tmp;

                isRotational = true;

                if (!isRandom)
                {
                    // アニメーションあり
                    pipe.transform.DORotate(new Vector3(0, 0, -90), rotationAnimDuration)
                        .SetRelative(true)
                        .OnComplete(() => isRotational = false);
                }
                else
                {
                    // アニメーションなし
                    pipe.transform.Rotate(new Vector3(0, 0, -90));
                    isRotational = false;
                }

            }
            else
            {
                Transform tmp = _directions[0];
                _directions[0] = _directions[1];
                _directions[1] = _directions[2];
                _directions[2] = _directions[3];
                _directions[3] = tmp;

                if (!isRandom)
                {
                    // アニメーションあり
                    pipe.transform.DORotate(new Vector3(0, 0, 90), rotationAnimDuration)
                        .SetRelative(true)
                        .OnComplete(() => isRotational = false);
                }
                else
                {
                    // アニメーションなし
                    pipe.transform.Rotate(new Vector3(0, 0, 90));
                    isRotational = false;
                }
            }

        }
    }

    public List<TravelDirection> GetPipeDirections()
    {
        List<TravelDirection> directions = new List<TravelDirection>();

        for (int i = 0; i < 4; i++)
        {
            if (_directions[i] != null) directions.Add((TravelDirection)i);
        }

        return directions;
    }

    /// <summary>
    /// パイプの向きを初期化する
    /// </summary>
    public virtual void InitializePipeDirections()
    {

    }

    public void SetCanRotational(bool canRotational)
    {
        this.canRotational = canRotational;
        Debug.Log("回転可能か否か：" + this.canRotational);
    }
}
