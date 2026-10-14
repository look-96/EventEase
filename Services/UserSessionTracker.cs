namespace EventEase.Services;

public sealed class UserSessionTracker
{
    public Guid SessionId { get; } = Guid.NewGuid();
    public DateTime StartedAtUtc { get; } = DateTime.UtcNow;
    public string? DisplayName { get; private set; }
    public int RegistrationsMade { get; private set; }

    public event Action? Changed;

    public void RecordRegistration(string attendeeName)
    {
        if (string.IsNullOrWhiteSpace(attendeeName))
        {
            return;
        }

        DisplayName = attendeeName.Trim();
        RegistrationsMade++;
        Changed?.Invoke();
    }

    public void Reset()
    {
        DisplayName = null;
        RegistrationsMade = 0;
        Changed?.Invoke();
    }
}
