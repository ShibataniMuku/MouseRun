using UnityEngine;
using Zenject;
using DG.Tweening;

public class ScoreItem : MonoBehaviour, IPickUpable
{
    private ScoreItemManager _scoreItemManager;
    private ItemGeterAnimationManager _itemGeterAnimationManager;

    private Grid _grid;
    private Tweener _tweener;
    
    [SerializeField]
    private int _scoreValue = 1;

    public void Init(ScoreItemManager scoreItemManager, ItemGeterAnimationManager itemGeterAnimationManager)
    {
        _scoreItemManager = scoreItemManager;
        _itemGeterAnimationManager = itemGeterAnimationManager;
        // PlayItemUpDownAnimation();
    }

    public void ResetPosition(FieldPosition fieldPosition)
    {
        _tweener.Kill();
        _grid = fieldPosition.grid;
        transform.position = fieldPosition.pos;
        PlayItemUpDownAnimation();
    }

    private void PlayItemUpDownAnimation()
    {
        _tweener = transform.DOMove(new Vector3(0, 0.1f, 0), 2)
            .SetRelative(true)
            .SetEase(Ease.InOutQuad)
            .SetLoops(-1, LoopType.Yoyo);
    }

    public void PickUp()
    {
        // アイテム獲得時のアニメーションを再生
        _itemGeterAnimationManager.PlayAnimation(_grid);
        // 取得したことをマネージャーに通知
        _scoreItemManager.PickUpItem(_grid, new Score(_scoreValue));
        
        Debug.Log($"{_grid.x}, {_grid.y}のアイテムを取得した。");
        
        gameObject.SetActive(false);
    }
}
