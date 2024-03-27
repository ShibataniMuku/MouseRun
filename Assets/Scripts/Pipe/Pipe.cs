using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Pipe : MonoBehaviour, IPointerClickHandler
{
    [SerializeField, Header("��]����")]
    private Way way;
    [SerializeField, Header("��]����")]
    private float rotationAnimDuration = 0.4f;

    [SerializeField]
    protected Transform[] point = new Transform[3];
    [SerializeField]
    private GameObject pipe;

    protected Transform[] _directions = new Transform[4]; // �p�C�v�̕����i0:��A1:�E�A2:���A3:���j
    private bool isRotational = false; // ���݉�]���Ă��邩�ۂ�
    private bool isOnCharacter = false; // ���݃p�C�v�̏�ɃL���������邪�ۂ�
    private bool canRotational = false; // ��]�����邱�Ƃ��ł��邩�ۂ�

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
    /// ���ɂ��̃p�C�v�ɓn�邱�Ƃ��ł��邩�ۂ��𒲍����A�\�ł���΃p�X�Ƀ|�C���g��ǉ�
    /// </summary>
    /// <param name="path">�ړ��o�H�̃p�X</param>
    /// <param name="travelDirection">���̃p�C�v�ɐN�����Ă������</param>
    public bool ResetPathPoint(List<Transform> path, TravelDirection travelDirection)
    {
        // �����q�����Ă���A����]���łȂ���Όo�H���X�V
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
    /// �Е�����N�������ۂɁA�����Е��͂ǂ̕�������Ԃ�
    /// </summary>
    /// <param name="pos">�}�X���W</param>
    /// <param name="travelDirection">�����̕���</param>
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
    /// �p�C�v���N���b�N���ꂽ�ۂɌĂ΂��
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (isRotational)
        {
            Debug.Log("��]���͉�]�ł��܂���");
            return;
        }
        if (isOnCharacter)
        {
            Debug.Log("�L�����N�^�[������Ă���Ƃ��͉�]�ł��܂���");
            return;
        }
        if (!canRotational)
        {
            Debug.Log("���݁A��]�����邱�Ƃ��ł��܂���");
            return;
        }

        RotatePipe(way, false);
    }

    /// <summary>
    /// �L����������Ă��邩�ۂ���ݒ�
    /// </summary>
    /// <param name="isOn">true:����Ă���, false;����Ă��Ȃ�</param>
    public void SetIsOnCharacter(bool isOn)
    {
        isOnCharacter = isOn;
    }

    /// <summary>
    /// �p�C�v����]������
    /// </summary>
    /// <param name="way">��]�̕���</param>
    /// <param name="isRandom">�����_���ɉ�]�����邩�ۂ��itrue�̏ꍇ�A�A�j���[�V�����Ȃ��j</param>
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
                    // �A�j���[�V��������
                    pipe.transform.DORotate(new Vector3(0, 0, -90), rotationAnimDuration)
                        .SetRelative(true)
                        .OnComplete(() => isRotational = false);
                }
                else
                {
                    // �A�j���[�V�����Ȃ�
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
                    // �A�j���[�V��������
                    pipe.transform.DORotate(new Vector3(0, 0, 90), rotationAnimDuration)
                        .SetRelative(true)
                        .OnComplete(() => isRotational = false);
                }
                else
                {
                    // �A�j���[�V�����Ȃ�
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
    /// �p�C�v�̌���������������
    /// </summary>
    public virtual void InitializePipeDirections()
    {

    }

    public void SetCanRotational(bool canRotational)
    {
        this.canRotational = canRotational;
        Debug.Log("��]�\���ۂ��F" + this.canRotational);
    }
}
