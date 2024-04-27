using UnityEngine;
using Zenject;

public class ScoreItem : MonoBehaviour, IPickUpable
{
    private ScoreItemManager _scoreItemManager;

    private int _posX = 0;
    private int _posY = 0;

    [SerializeField]
    private int _scoreValue = 1;

    public void Init(ScoreItemManager scoreItemManager)
    {
        _scoreItemManager = scoreItemManager;
    }

    public void InitPosition(Grid grid)
    {
        _posX = grid.x;
        _posY = grid.y;
    }

    public void PickUp()
    {
        // Žæ“¾‚µ‚½‚±‚Æ‚ð’Ê’m
        _scoreItemManager.PickUpItem(_posX, _posY, new Score(_scoreValue));

        Debug.Log(_scoreValue + " “_Šl“¾");

        gameObject.SetActive(false);
    }
}
