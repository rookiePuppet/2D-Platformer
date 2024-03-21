public class Dialog : UIBase
{
    protected override int SortingOrder { get; set; } = 2;

    public override void Show()
    {
        gameObject.SetActive(true);
    }

    public override void Hide()
    {
        gameObject.SetActive(false);
    }
}
