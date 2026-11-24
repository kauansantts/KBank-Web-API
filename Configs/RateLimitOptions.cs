namespace KBank_Web_API;

public class RateLimitOptions
{
    public int PermitLimit { get; set; } = 5;
    public int QueueLimit { get; set; } = 0;
    public int WindowSeconds { get; set; } = 10;
    public bool AutoReplenishment { get; set; } = false;
}
