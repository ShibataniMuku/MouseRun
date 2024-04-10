using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultPhase : MonoBehaviour, IPhase
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public async UniTask RunPhase()
    {
        // ブラックイン
        await SceneTransitioner.sceneTransitionerInstance.CompleteTransitionScene();


    }
}
