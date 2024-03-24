using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Characters : MonoBehaviour
{
    [SerializeField, Header("�ړ����x")]
    private float _speed = 1;

    private List<Transform> _path = new List<Transform>(); // �ʉߓ_
    private TravelDirection _travelDirection; // �i�s����
    private Vector2Int _currentLocation; // ���ݒn
    private Sequence _moveSq;
    private Transform _trans; // �L�����N�^�[��transform
    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        _trans = transform;

        // �}�E�X�̏����ʒu������
        int posX = (int)(PipeManager.pipes.GetLength(0) / 2);
        int posY = (int)(PipeManager.pipes.GetLength(1) / 2);
        _trans.position = PipeManager.pipes[posX, posY].gameObject.transform.position;
        _currentLocation = new Vector2Int(posX, posY); // ���ݒn
        _travelDirection = PipeManager.pipes[posX, posY].GetPipeDirections()[Random.Range(0, 2)]; // ���݂̐i�s����
        ResetPath();
    }

    /// <summary>
    /// �ړ��o�H�̃p�X���Đݒ�
    /// </summary>
    void ResetPath()
    {
        switch (_travelDirection)
        {
            case TravelDirection.up:
                if (_currentLocation.y + 1 < PipeManager.pipes.GetLength(1))
                {
                    // �t�B�[���h�̒[�łȂ����
                    if (!PipeManager.pipes[_currentLocation.x, _currentLocation.y + 1].ResetPathPoint(_path, TravelDirection.down))
                    {
                        // �����q�����Ă��Ȃ���Έ����Ԃ�
                        PipeManager.pipes[_currentLocation.x, _currentLocation.y].ResetPathPoint(_path, _travelDirection);
                        _travelDirection = TravelDirection.down;
                    }
                    else
                    {
                        // �����q�����Ă���Ό��ݒn���X�V
                        PipeManager.pipes[_currentLocation.x, _currentLocation.y].SetIsOnCharacter(false);
                        _currentLocation += new Vector2Int(0, 1);
                        PipeManager.pipes[_currentLocation.x, _currentLocation.y].SetIsOnCharacter(true);
                    }
                }
                else
                {
                    // �t�B�[���h�̒[�Ȃ�����Ԃ�
                    PipeManager.pipes[_currentLocation.x, _currentLocation.y].ResetPathPoint(_path, _travelDirection);
                    _travelDirection = TravelDirection.down;
                }
                break;
            case TravelDirection.down:
                if (_currentLocation.y - 1 >= 0)
                {

                    if (!PipeManager.pipes[_currentLocation.x, _currentLocation.y - 1].ResetPathPoint(_path, TravelDirection.up))
                    {
                        PipeManager.pipes[_currentLocation.x, _currentLocation.y].ResetPathPoint(_path, _travelDirection);
                        _travelDirection = TravelDirection.up;
                    }
                    else
                    {
                        PipeManager.pipes[_currentLocation.x, _currentLocation.y].SetIsOnCharacter(false);
                        _currentLocation += new Vector2Int(0, -1);
                        PipeManager.pipes[_currentLocation.x, _currentLocation.y].SetIsOnCharacter(true);
                    }
                }
                else
                {
                    PipeManager.pipes[_currentLocation.x, _currentLocation.y].ResetPathPoint(_path, _travelDirection);
                    _travelDirection = TravelDirection.up;
                }
                break;
            case TravelDirection.right:
                if (_currentLocation.x + 1 < PipeManager.pipes.GetLength(0))
                {

                    if (!PipeManager.pipes[_currentLocation.x + 1, _currentLocation.y].ResetPathPoint(_path, TravelDirection.left))
                    {
                        PipeManager.pipes[_currentLocation.x, _currentLocation.y].ResetPathPoint(_path, _travelDirection);
                        _travelDirection = TravelDirection.left;
                    }
                    else
                    {
                        PipeManager.pipes[_currentLocation.x, _currentLocation.y].SetIsOnCharacter(false);
                        _currentLocation += new Vector2Int(1, 0);
                        PipeManager.pipes[_currentLocation.x, _currentLocation.y].SetIsOnCharacter(true);
                    }
                }
                else
                {
                    PipeManager.pipes[_currentLocation.x, _currentLocation.y].ResetPathPoint(_path, _travelDirection);
                    _travelDirection = TravelDirection.left;
                }
                break;
            case TravelDirection.left:
                if (_currentLocation.x - 1 >= 0)
                {

                    if (!PipeManager.pipes[_currentLocation.x - 1, _currentLocation.y].ResetPathPoint(_path, TravelDirection.right))
                    {
                        PipeManager.pipes[_currentLocation.x, _currentLocation.y].ResetPathPoint(_path, _travelDirection);
                        _travelDirection = TravelDirection.right;
                    }
                    else
                    {
                        PipeManager.pipes[_currentLocation.x, _currentLocation.y].SetIsOnCharacter(false);
                        _currentLocation += new Vector2Int(-1, 0);
                        PipeManager.pipes[_currentLocation.x, _currentLocation.y].SetIsOnCharacter(true);
                    }
                }
                else
                {
                    PipeManager.pipes[_currentLocation.x, _currentLocation.y].ResetPathPoint(_path, _travelDirection);
                    _travelDirection = TravelDirection.right;
                }
                break;
        }

        //Debug.Log("���݂̐i�s������ " + _travelDirection);
        //Debug.Log("���݂̍��W�� " + _currentLocation);
        SetTravelDirection(_currentLocation);
        AnimateCharacter();
    }

    /// <summary>
    /// �L�����N�^�[�𓮂���
    /// </summary>
    private void AnimateCharacter()
    {
        Vector3[] movePath = new Vector3[_path.Count];

        for (int i = 0; i < _path.Count; i++)
        {
            movePath[i] = _path[i].position;
        }

        _moveSq = DOTween.Sequence()
            .Append(transform.DOPath(movePath, _speed, PathType.CatmullRom, PathMode.Sidescroller2D)
            .SetLookAt(0.05f, Vector3.forward)
            .SetEase(Ease.Linear));

        _moveSq.Play();
        _moveSq.OnComplete(() => { ResetPath(); });
    }

    /// <summary>
    /// ���݂̐i�s����
    /// </summary>
    /// <param name="pos">���̃}�X���W</param>
    public void SetTravelDirection(Vector2Int pos)
    {
        TravelDirection t = _travelDirection;
        switch (_travelDirection)
        {
            case TravelDirection.up:
                t = TravelDirection.down; break;
            case TravelDirection.down:
                t = TravelDirection.up; break;
            case TravelDirection.right:
                t = TravelDirection.left; break;
            case TravelDirection.left:
                t = TravelDirection.right; break;
        }

        _travelDirection = PipeManager.pipes[pos.x, pos.y].GetExitPoint(pos, t);
    }

    /// <summary>
    /// ���_��ǉ�
    /// </summary>
    /// <param name="score">���_</param>
    public void AddScore(int score)
    {
        //_gameManager.AddScore(score);
    }
}