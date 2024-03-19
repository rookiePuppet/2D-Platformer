using UnityEngine;

public class Item : MonoBehaviour
{
    [field: SerializeField] public ItemDataSO Data { get; private set; }

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if (Data is null) return;
        Initialize(Data);
    }

    public void Initialize(ItemDataSO itemData)
    {
        Data = itemData;

        _spriteRenderer.sprite = itemData.itemSprite;
    }
}