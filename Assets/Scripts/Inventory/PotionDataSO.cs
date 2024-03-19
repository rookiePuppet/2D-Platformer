using UnityEngine;

[CreateAssetMenu(fileName = "newPotionData", menuName = "Inventory/Potion")]
public class PotionDataSO : ItemDataSO
{
    public PotionGainType gainType;
    public int gainValue;

    private void OnEnable()
    {
        itemType = ItemType.Potion;
    }
}

public enum PotionGainType
{
    Health,
    DashEnergy
}