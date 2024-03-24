using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IPhase
{
    UniTask OnCompleteTransition();
    UniTask OnStartTransition();
}