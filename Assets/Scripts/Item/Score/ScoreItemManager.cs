using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class ScoreItemManager : MonoBehaviour
{
    [Header("���������܂ł̎���")]
    [SerializeField]
    float _separateGeneratingItem = 3;
    [Header("�����̔z�u��")]
    private int _defaultSettingCount = 8;

    [SerializeField]
    GameObject _scoreItem;
    [SerializeField]
    Transform _ScoreItemParent;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < _defaultSettingCount; i++)
        {
            GenerateScoreItem();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WaitGeneratingScoreItem()
    {
        Invoke("GenerateScoreItem", _separateGeneratingItem);
    }

    public void GenerateScoreItem()
    {
        int posX = Random.Range(0, PipeManager.pipes.GetLength(0));
        int posY = Random.Range(0, PipeManager.pipes.GetLength(1));

        //�A�N�e�B�u�łȂ��I�u�W�F�N�g��bullets�̒�����T��
        foreach (Transform t in _ScoreItemParent)
        {
            if (!t.gameObject.activeSelf)
            {
                //��A�N�e�B�u�ȃI�u�W�F�N�g�̈ʒu�Ɖ�]��ݒ�
                t.SetPositionAndRotation(PipeManager.pipes[posX, posY].transform.position, Quaternion.identity);
                //�A�N�e�B�u�ɂ���
                t.gameObject.SetActive(true);
                return;
            }
        }

        //��A�N�e�B�u�ȃI�u�W�F�N�g���Ȃ��ꍇ�V�K����
        //��������bullets�̎q�I�u�W�F�N�g�ɂ���
        Instantiate(_scoreItem, PipeManager.pipes[posX, posY].transform.position, Quaternion.identity, _ScoreItemParent);
    }
}
