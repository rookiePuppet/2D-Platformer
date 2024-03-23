using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameLevelsData", menuName = "Data/GameLevelsData")]
public class GameLevelsDataSO: ScriptableObject
{
    public List<GameLevelData> data;
}

[System.Serializable]
public struct GameLevelData
{
    [Header("Basic Config")]
    public int id;
    public string levelName;
    public Sprite previewImage;
    public string sceneName;

    [Header("Player")]
    public Vector3 playerInitialPosition;
    public WeaponDataSO defaultWeapon;
}