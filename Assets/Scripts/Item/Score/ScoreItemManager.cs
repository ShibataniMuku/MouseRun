using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System;
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
            List<Grid> isNotPlaced = _itemManager.GetItemStatusList();

            int grid =  UnityEngine.Random.Range(0, isNotPlaced.Count);
            GenerateItem(isNotPlaced[grid]);
        }
    }

    public void GenerateItem(Grid grid)
    {
        if (_itemManager.GetItemStatus(grid))
        {
            Debug.LogError("���ɃA�C�e�����z�u����Ă��鑫��ɁA�d�����ăA�C�e����z�u���悤�Ƃ��Ă��܂��B");
            return;
        }

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
        _itemManager.SetItemStatus(new Grid(grid.x, grid.y));
    }

    /// <summary>
    /// �A�C�e���������_���ɐ�������
    /// </summary>
    /// <param name="delayTime">���b��ɐ������邩</param>
    public async void GenerateItem(float delayTime)
    {
        float time = UnityEngine.Random.Range(delayTime - 3, delayTime + 3);

        List<Grid> isNotPlaced = _itemManager.GetItemStatusList();
        int grid = UnityEngine.Random.Range(0, isNotPlaced.Count);

        await UniTask.Delay(TimeSpan.FromSeconds(time));

        GenerateItem(isNotPlaced[grid]);
    }

    public void PickUpItem(int posX, int posY, Score score)
    {
        _scoreManager.AddScore(score);
        // �A�C�e�������폜
        _itemManager.RemoveItem(new Grid(posX, posY));

        GenerateItem(_separateGeneratingItem);
    }
}
