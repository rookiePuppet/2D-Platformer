public interface ISwitchable
{
    bool IsActive { get; }
    void Activate();
    void Deactivate();
}