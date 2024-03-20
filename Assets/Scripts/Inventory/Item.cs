using UnityEngine;

public class Item : MonoBehaviour
{
    [field: SerializeField] public ItemDataSO Data { get; private set; }

    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider;

    private float ItemHeight => _boxCollider.size.y;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        if (Data is null) return;
        Initialize(Data);

        var hit = Physics2D.Raycast(transform.position, Vector2.down, 10f, 1 << LayerMask.NameToLayer("Ground"));
        transform.position = hit.point + Vector2.up * ItemHeight;
        print(hit.point);
    }

    public void Initialize(ItemDataSO itemData)
    {
        Data = itemData;

        _spriteRenderer.sprite = itemData.itemSprite;
        _boxCollider.size = _spriteRenderer.size;
    }
}