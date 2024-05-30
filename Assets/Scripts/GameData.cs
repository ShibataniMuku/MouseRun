using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/CreateGameParamAsset")]
public class GameData : ScriptableObject
{
    public Vector2Int gridCount = new Vector2Int(6, 7);
}
