﻿using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using Zenject;

public class Characters : MonoBehaviour
{
    [SerializeField, Header("移動速度")]
    private float _speed = 1;

    [Inject]
    private PipeManager _pipeManager;
    [Inject]
    private PlayingPhase _playingPhase;

    private List<Transform> _path = new List<Transform>(); // 通過点
    private TravelDirection _travelDirection; // 進行方向
    private Vector2Int _currentLocation; // 現在地
    private Sequence _moveSq;
    private Transform _trans; // キャラクターのtransform

    // Start is called before the first frame update
    void Start()
    {
        _playingPhase.SetOnStartGame(() =>
        {
            _moveSq.Restart();
            return UniTask.CompletedTask;
        });

        _playingPhase.SetOnFinishGame(() =>
        {
            _moveSq.Pause();
            return UniTask.CompletedTask;
        });

        _trans = transform;

        // マウスの初期位置を決定
        int posX = (int)(_pipeManager.pipes.GetLength(0) / 2);
        int posY = (int)(_pipeManager.pipes.GetLength(1) / 2);
        _trans.position = _pipeManager.pipes[posX, posY].gameObject.transform.position;
        _currentLocation = new Vector2Int(posX, posY); // 現在地
        _travelDirection = _pipeManager.pipes[posX, posY].GetPipeDirections()[Random.Range(0, 2)]; // 現在の進行方向
        ResetPath();
        _moveSq.Pause();
    }

    /// <summary>
    /// 移動経路のパスを再設定
    /// </summary>
    void ResetPath()
    {
        switch (_travelDirection)
        {
            case TravelDirection.up:
                if (_currentLocation.y + 1 < _pipeManager.pipes.GetLength(1))
                {
                    // フィールドの端でなければ
                    if (!_pipeManager.pipes[_currentLocation.x, _currentLocation.y + 1].ResetPathPoint(_path, TravelDirection.down))
                    {
                        // 道が繋がっていなければ引き返す
                        _pipeManager.pipes[_currentLocation.x, _currentLocation.y].ResetPathPoint(_path, _travelDirection);
                        _travelDirection = TravelDirection.down;
                    }
                    else
                    {
                        // 道が繋がっていれば現在地を更新
                        _pipeManager.pipes[_currentLocation.x, _currentLocation.y].SetIsOnCharacter(false);
                        _currentLocation += new Vector2Int(0, 1);
                        _pipeManager.pipes[_currentLocation.x, _currentLocation.y].SetIsOnCharacter(true);
                    }
                }
                else
                {
                    // フィールドの端なら引き返す
                    _pipeManager.pipes[_currentLocation.x, _currentLocation.y].ResetPathPoint(_path, _travelDirection);
                    _travelDirection = TravelDirection.down;
                }
                break;
            case TravelDirection.down:
                if (_currentLocation.y - 1 >= 0)
                {

                    if (!_pipeManager.pipes[_currentLocation.x, _currentLocation.y - 1].ResetPathPoint(_path, TravelDirection.up))
                    {
                        _pipeManager.pipes[_currentLocation.x, _currentLocation.y].ResetPathPoint(_path, _travelDirection);
                        _travelDirection = TravelDirection.up;
                    }
                    else
                    {
                        _pipeManager.pipes[_currentLocation.x, _currentLocation.y].SetIsOnCharacter(false);
                        _currentLocation += new Vector2Int(0, -1);
                        _pipeManager.pipes[_currentLocation.x, _currentLocation.y].SetIsOnCharacter(true);
                    }
                }
                else
                {
                    _pipeManager.pipes[_currentLocation.x, _currentLocation.y].ResetPathPoint(_path, _travelDirection);
                    _travelDirection = TravelDirection.up;
                }
                break;
            case TravelDirection.right:
                if (_currentLocation.x + 1 < _pipeManager.pipes.GetLength(0))
                {

                    if (!_pipeManager.pipes[_currentLocation.x + 1, _currentLocation.y].ResetPathPoint(_path, TravelDirection.left))
                    {
                        _pipeManager.pipes[_currentLocation.x, _currentLocation.y].ResetPathPoint(_path, _travelDirection);
                        _travelDirection = TravelDirection.left;
                    }
                    else
                    {
                        _pipeManager.pipes[_currentLocation.x, _currentLocation.y].SetIsOnCharacter(false);
                        _currentLocation += new Vector2Int(1, 0);
                        _pipeManager.pipes[_currentLocation.x, _currentLocation.y].SetIsOnCharacter(true);
                    }
                }
                else
                {
                    _pipeManager.pipes[_currentLocation.x, _currentLocation.y].ResetPathPoint(_path, _travelDirection);
                    _travelDirection = TravelDirection.left;
                }
                break;
            case TravelDirection.left:
                if (_currentLocation.x - 1 >= 0)
                {

                    if (!_pipeManager.pipes[_currentLocation.x - 1, _currentLocation.y].ResetPathPoint(_path, TravelDirection.right))
                    {
                        _pipeManager.pipes[_currentLocation.x, _currentLocation.y].ResetPathPoint(_path, _travelDirection);
                        _travelDirection = TravelDirection.right;
                    }
                    else
                    {
                        _pipeManager.pipes[_currentLocation.x, _currentLocation.y].SetIsOnCharacter(false);
                        _currentLocation += new Vector2Int(-1, 0);
                        _pipeManager.pipes[_currentLocation.x, _currentLocation.y].SetIsOnCharacter(true);
                    }   
                }
                else
                {
                    _pipeManager.pipes[_currentLocation.x, _currentLocation.y].ResetPathPoint(_path, _travelDirection);
                    _travelDirection = TravelDirection.right;
                }
                break;
        }

        //Debug.Log("現在の進行方向は " + _travelDirection);
        //Debug.Log("現在の座標は " + _currentLocation);
        SetTravelDirection(_currentLocation);
        AnimateCharacter();
    }

    /// <summary>
    /// キャラクターを動かす
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
    /// 現在の進行方向
    /// </summary>
    /// <param name="pos">次のマス座標</param>
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

        _travelDirection = _pipeManager.pipes[pos.x, pos.y].GetExitPoint(pos, t);
    }
}