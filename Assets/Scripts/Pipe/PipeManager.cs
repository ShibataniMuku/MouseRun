using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PipeManager : MonoBehaviour
{
    [SerializeField, Header("�p�C�v�̃}�X��")]
    Vector2Int gridCount;

    [SerializeField]
    private PipeType[] pipeType = new PipeType[2];
    [SerializeField]
    RectTransform fieldArea; // �p�C�v���\�����ꂤ��G���A
    [SerializeField]
    Transform pipeParent; // �p�C�v�̐e�I�u�W�F�N�g

    GameObject[,] pipeObjects;
    
    public static Pipe[,] pipes;

    private void Awake()
    {
        pipeObjects = new GameObject[gridCount.x, gridCount.y];
        pipes = new Pipe[gridCount.x, gridCount.y];
        GeneratePipes(gridCount);
    }

    // Start is called before the first frame update
    void Start()
    {
        // ����\�t�F�[�Y�ɂȂ�����A��]�\�ɂ���
        PlayingPhase.playingPhaseInstance.SetOnStartGame(() =>
        {
            foreach (Pipe pipe in pipes) pipe.SetCanRotational(true);
            return UniTask.CompletedTask;
        });

        // ����s�\�t�F�[�Y�ɂȂ�����A��]�s�\�ɂ���
        PlayingPhase.playingPhaseInstance.SetOnFinishGame(() =>
        {
            foreach (Pipe pipe in pipes) pipe.SetCanRotational(false);
            return UniTask.CompletedTask;
        });
    }

    /// <summary>
    /// �t�B�[���h�𐶐�����
    /// </summary>
    /// <param name="fieldGrid">�t�B�[���h�̃T�C�Y</param>
    private void GeneratePipes(Vector2Int gridCount)
    {
        float fieldSizeX = Screen.width - fieldArea.offsetMin.x - fieldArea.offsetMax.x;
        float fieldSizeY = Screen.width - fieldArea.offsetMin.y - fieldArea.offsetMax.y;
        Vector2 fieldSize = new Vector2(fieldSizeX, fieldSizeY ); // �p�C�v���\�����ꂤ��G���A�̑傫��
        float gridSize = 0; // 1�}�X���̑傫��
        Vector2 margin; // ��ʏc����ɂ��]��

        Debug.Log(fieldSize.x + "   " + fieldSize.y);

        // �c���̒����ɂ���āA���ɗ]������邩�A�c�ɗ]������邩������
        if ((fieldSize.x) / gridCount.x <= (fieldSize.y) / gridCount.y)
        {
            gridSize = fieldSize.x / gridCount.x;
        }
        else
        {
            gridSize = fieldSize.y / gridCount.y;
        }

        // �p�C�v�̔z�u�������ɍ��킹��
        margin.x = fieldSize.x - gridSize * gridCount.x;
        margin.y = fieldSize.y - gridSize * gridCount.y;

        for (int iy = 0; iy < gridCount.y; iy++)
        {
            for (int jx = 0; jx < gridCount.x; jx++)
            {
                float screenPosX = margin.x + gridSize / 2 + gridSize * jx;
                float screenPosY = margin.y + gridSize / 2 + gridSize * iy;
                Vector3 screenPos = new Vector3(screenPosX, screenPosY, 0);
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos) - new Vector3(0, 0, Camera.main.transform.position.z);
                
                pipeObjects[jx, iy] = Instantiate(DecidePipeType(jx, iy), pipeParent);
                Transform t = pipeObjects[jx, iy].transform;
                t.position = worldPos;
                t.localScale = Vector3.one * gridSize / 150;
                pipes[jx, iy] = pipeObjects[jx, iy].GetComponent<Pipe>();
                pipes[jx, iy].InitializePipeDirections();
            }
        }
    }

    /// <summary>
    /// �p�C�v�̎�ނ����肷��
    /// </summary>
    /// <param name="x">x�}�X���W</param>
    /// <param name="y">y�}�X���W</param>
    /// <returns></returns>
    private GameObject DecidePipeType(int x, int y)
    {
        int num = Random.Range(0, 100);

        if ((x == 0 && (y == 0 || y == gridCount.y - 1)) || (x == gridCount.x - 1 && (y == 0 || y == gridCount.y - 1)))
        {
            // �l�p
            if (num <= (pipeType[0].edgeProbability / (pipeType[0].edgeProbability + pipeType[1].edgeProbability)) * 100)
            {
                return pipeType[0].pipeObj;
            }
            else
            {
                return pipeType[1].pipeObj;
            }
        }
        else if (x == gridCount.x - 1 || x == 0 || y == gridCount.y - 1 || y == 0)
        {
            // �O��
            if (num <= (pipeType[0].outProbability / (pipeType[0].outProbability + pipeType[1].outProbability)) * 100)
            {
                return pipeType[0].pipeObj;
            }
            else
            {
                return pipeType[1].pipeObj;
            }
        }
        else
        {
            // ����
            if (num <= (pipeType[0].inProbability / (pipeType[0].inProbability + pipeType[1].inProbability)) * 100)
            {
                return pipeType[0].pipeObj;
            }
            else
            {
                return pipeType[1].pipeObj;
            }
        }
    }
}