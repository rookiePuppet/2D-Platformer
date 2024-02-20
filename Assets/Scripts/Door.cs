using UnityEngine;
using UnityEngine.Tilemaps;

public class Door : MonoBehaviour, ISwitchable
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private TileBase openTile;
    [SerializeField] private TileBase closedTile;

    [SerializeField] private bool isOpenDefault = false;

    public bool IsActive { get; private set; } = false;

    private Vector3Int _targetCellPos;

    private void Awake()
    {
        _targetCellPos = tilemap.WorldToCell(transform.position);
        tilemap.SetTile(_targetCellPos, isOpenDefault ? openTile : closedTile);
    }

    public void Activate()
    {
        tilemap.SetTile(_targetCellPos, openTile);
        IsActive = true;
    }

    public void Deactivate()
    {
        tilemap.SetTile(_targetCellPos, closedTile);
        IsActive = false;
    }
}
