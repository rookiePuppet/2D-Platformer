using System;
using Cinemachine;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelInitializer", menuName = "Tool/LevelInitializer")]
public class LevelInitializer : ScriptableObject
{
    [SerializeField] private GameObject playerPrefab;
    
    public async void InitializeLevel(GameLevelData levelData, Action loadMainView)
    {
        var playerObject = Instantiate(playerPrefab, levelData.playerInitialPosition, Quaternion.identity);

        loadMainView?.Invoke();
        
        // 设置摄像机跟随
        var camera = FindAnyObjectByType<CinemachineVirtualCamera>();
        camera.Follow = playerObject.transform;

        await Awaitable.NextFrameAsync();
        
        // 设置初始武器
        var playerController = playerObject.GetComponent<PlayerController>();
        playerController.WeaponsHolder.InitializeWeapon(levelData.defaultWeaponId);
    }
}