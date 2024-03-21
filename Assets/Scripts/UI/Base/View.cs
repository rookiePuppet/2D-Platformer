public class View : UIBase
{
    protected override int SortingOrder { get; set; } = 0;

    public override void Show()
    {
        gameObject.SetActive(true);
    }

    public override void Hide()
    {
        gameObject.SetActive(false);
    }
}