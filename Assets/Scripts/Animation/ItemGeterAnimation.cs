using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class ItemGeterAnimation : MonoBehaviour
{
    [SerializeField] private int particleCount = 8;
    [SerializeField] private float particleRadius = 0.5f;
    [SerializeField] private float expandDuration = 0.4f;
    [SerializeField] private float expandScale = 0.2f;
    [SerializeField] private float shrinkDuration = 0.5f;
    [SerializeField] private List<GameObject> particles;

    [Inject] private PipeManager _pipeManager;
    
    // Start is called before the first frame update
    void Start()
    {
        if (particleCount < particles.Count) Debug.LogError("アイテム獲得時のパーティクルの数が不足しています。");
        gameObject.SetActive(false);
    }

    public void Init(PipeManager pipeManager)
    {
        _pipeManager = pipeManager;
    }

    /// <summary>
    /// アイテム獲得時のアニメーションを再生する
    /// </summary>
    /// <param name="grid">マス座標</param>
    public void PlayAnimation(Grid grid)
    {
        gameObject.SetActive(true);
        
        transform.position = _pipeManager.pipes[grid.x, grid.y].transform.position;
        
        for (int i = 0; i < particleCount; i++)
        {
            Transform tr = particles[i].transform;
            tr.localPosition = Vector3.zero;
            tr.rotation = Quaternion.Euler(new Vector3(0, 0, (360f / particleCount) * i + 90));
            tr.localScale = Vector3.zero;

            float particleAngleUnit = 2 * Mathf.PI / particleCount;
            Vector3 destination = new Vector3(Mathf.Cos(particleAngleUnit * i), Mathf.Sin(particleAngleUnit * i), 0) * particleRadius;
            tr.DOMove(destination, expandDuration).SetRelative(true).SetEase(Ease.OutQuart);
            tr.DOScale(expandScale, expandDuration).SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    tr.DOScale(Vector3.zero, shrinkDuration).SetDelay(0.3f);
                    tr.DORotate(tr.rotation.eulerAngles + new Vector3(0, 0, 360), shrinkDuration, RotateMode.FastBeyond360)
                        .OnComplete(() => gameObject.SetActive(false));
                    
                    Debug.Log("アニメーションが終了" + expandDuration);
                });
        }
        
        Debug.Log("アイテム獲得アニメーションを再生しました");
    }
}
