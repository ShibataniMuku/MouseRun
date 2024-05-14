using UnityEngine;
using Zenject;

public class TimeItemManager : MonoBehaviour, IItemManager
{
    //[Inject]
    private TimeManager _timeManager;
    [Inject]
    private ItemManager _itemManager;
    [Inject]
    private PipeManager _pipeManager;

    [SerializeField]
    GameObject _timeItem;
    [SerializeField]
    Transform _timeItemParent;

    public TimeItemManager(TimeManager timeManager, ItemManager itemManager)
    {
        _timeManager = timeManager;
        _itemManager = itemManager;

        InitFieldItem();
    }

    public void InitFieldItem()
    {
        // ���ԉ����̃A�C�e���́A�����z�u���Ȃ�
    }

    public void GenerateItem(Grid grid)
    {
        // ����������W�́AItemManager�ނ��Ǘ�����ItemManager�Őݒ�I�I�I�I�I�I�I�I�I�I

        //�A�N�e�B�u�łȂ��I�u�W�F�N�g��bullets�̒�����T��
        foreach (Transform t in _timeItemParent)
        {
            if (!t.gameObject.activeSelf)
            {
                //��A�N�e�B�u�ȃI�u�W�F�N�g�̈ʒu�Ɖ�]��ݒ�
                t.SetPositionAndRotation(_pipeManager.pipes[grid.x, grid.y].transform.position, Quaternion.identity);
                //�A�N�e�B�u�ɂ���
                t.gameObject.SetActive(true);
                return;
            }
        }

        //��A�N�e�B�u�ȃI�u�W�F�N�g���Ȃ��ꍇ�V�K����
        //��������bullets�̎q�I�u�W�F�N�g�ɂ���
        Instantiate(_timeItem, _pipeManager.pipes[grid.x, grid.y].transform.position, Quaternion.identity, _timeItemParent);

        // �A�C�e������o�^
        _itemManager.SetItemStatus(new Grid(grid.x, grid.y));
    }

    public void PickUpItem(int posX, int posY, float time)
    {
        _timeManager.MainTimer.AddTime(new TimeLimit(time));
        // �A�C�e�������폜
        _itemManager.RemoveItem(new Grid(posX, posY));


        // ���̃A�C�e���𐶐����鏈�� �܂��� �A�C�e�����l���Č��������Ƃ�`����

    }
}
