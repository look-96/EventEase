using EventEase.Models;

namespace EventEase.Services;

public sealed class AttendanceTracker
{
    private readonly Dictionary<Guid, DateTime> _checkIns = new();

    public event Action? Changed;

    public bool IsCheckedIn(Guid registrationId) => _checkIns.ContainsKey(registrationId);

    public DateTime? GetCheckInTimeUtc(Guid registrationId) =>
        _checkIns.TryGetValue(registrationId, out var time) ? time : null;

    public int CountForEvent(int eventId, IEnumerable<EventRegistration> registrations)
    {
        return registrations.Count(registration =>
            registration.EventId == eventId && _checkIns.ContainsKey(registration.Id));
    }

    public bool TryCheckIn(Guid registrationId, IEnumerable<EventRegistration> registrations)
    {
        if (registrationId == Guid.Empty || _checkIns.ContainsKey(registrationId) ||
            !registrations.Any(registration => registration.Id == registrationId))
        {
            return false;
        }

        _checkIns.Add(registrationId, DateTime.UtcNow);
        Changed?.Invoke();
        return true;
    }
}
