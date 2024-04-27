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

    [SerializeField, Header("���������܂ł̎���")]
    private float _separateGeneratingItem = 3;
    [SerializeField, Header("�����̔z�u��")]
    private int _defaultSettingCount = 8;

    [SerializeField]
    GameObject _scoreItem;
    [SerializeField]
    Transform _scoreItemParent;

    public void Initialize()
    {
        Debug.Log("�X�R�A�A�C�e���̔z�u�����������܂���");

        InitFieldItem();
    }

    public void InitFieldItem()
    {
        // �A�C�e���̔z�u�̏�����
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
        // ����������W�́AItemManager�ނ��Ǘ�����ItemManager�Őݒ�I�I�I�I�I�I�I�I�I�I

        //�A�N�e�B�u�łȂ��I�u�W�F�N�g��bullets�̒�����T��
        foreach (Transform t in _scoreItemParent)
        {
            if (!t.gameObject.activeSelf)
            {
                //��A�N�e�B�u�ȃI�u�W�F�N�g�̈ʒu�Ɖ�]��ݒ�
                t.SetPositionAndRotation(_pipeManager.pipes[grid.x, grid.y].transform.position, Quaternion.identity);
                //�A�N�e�B�u�ɂ���
                t.gameObject.SetActive(true);

                t.gameObject.GetComponent<IPickUpable>().InitPosition(grid);
                return;
            }
        }

        //��A�N�e�B�u�ȃI�u�W�F�N�g���Ȃ��ꍇ�V�K����
        //��������bullets�̎q�I�u�W�F�N�g�ɂ���
        GameObject item = Instantiate(_scoreItem, _pipeManager.pipes[grid.x, grid.y].transform.position, Quaternion.identity, _scoreItemParent);
        item.GetComponent<IPickUpable>().InitPosition(grid);
        item.GetComponent<ScoreItem>().Init(this);

        // �A�C�e������o�^
        _itemManager.SetItem(new Grid(grid.x, grid.y));
    }

    public void PickUpItem(int posX, int posY, Score score)
    {
        _scoreManager.AddScore(score);
        // �A�C�e�������폜
        _itemManager.RemoveItem(new Grid(posX, posY));


        // ���̃A�C�e���𐶐����鏈�� �܂��� �A�C�e�����l���Č��������Ƃ�`����

    }
}
