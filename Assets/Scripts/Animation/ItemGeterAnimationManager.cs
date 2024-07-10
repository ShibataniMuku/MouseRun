using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ItemGeterAnimationManager : MonoBehaviour
{
    [SerializeField] private Transform _animationAcquiringItemParent;
    [SerializeField] private GameObject _animationAcquiringItem;
    [Inject] private PipeManager _pipeManager;

    public void PlayAnimation(Grid grid)
    {
        //アクティブでないオブジェクトをbulletsの中から探索
        foreach (Transform t in _animationAcquiringItemParent)
        {
            if (!t.gameObject.activeSelf)
            {
                //アクティブにする
                t.gameObject.SetActive(true);
                // アニメーションを再生
                t.gameObject.GetComponent<ItemGeterAnimation>().PlayAnimation(grid);
                return;
            }
        }

        //非アクティブなオブジェクトがない場合新規生成
        //生成時にbulletsの子オブジェクトにする
        GameObject item = Instantiate(_animationAcquiringItem, _pipeManager.pipes[grid.x, grid.y].transform.position, Quaternion.identity, _animationAcquiringItemParent);
        item.GetComponent<ItemGeterAnimation>().Init(_pipeManager);
        item.GetComponent<ItemGeterAnimation>().PlayAnimation(grid);
    }
}
