using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using EventEase.Data;
using EventEase.Models;

namespace EventEase.Services;

public enum RegistrationOutcome
{
    Added,
    Duplicate,
    Invalid
}

public sealed class RegistrationState
{
    private readonly List<EventRegistration> _registrations = [];
    private readonly HashSet<(int EventId, string NormalizedEmail)> _registrationKeys = [];
    private readonly ReadOnlyCollection<EventRegistration> _registrationsView;

    public RegistrationState()
    {
        _registrationsView = _registrations.AsReadOnly();
    }

    public IReadOnlyList<EventRegistration> Registrations => _registrationsView;

    public event Action? Changed;

    public int CountForEvent(int eventId) =>
        _registrations.Count(registration => registration.EventId == eventId);

    public IReadOnlyList<EventRegistration> GetForEvent(int eventId) =>
        _registrations.Where(registration => registration.EventId == eventId).ToList();

    public RegistrationOutcome TryAdd(EventRegistration registration)
    {
        if (registration is null || EventCatalog.GetById(registration.EventId) is null)
        {
            return RegistrationOutcome.Invalid;
        }

        var errors = new List<ValidationResult>();
        if (!Validator.TryValidateObject(registration, new ValidationContext(registration), errors, true))
        {
            return RegistrationOutcome.Invalid;
        }

        var normalizedEmail = registration.Email.Trim();
        var key = (registration.EventId, normalizedEmail.ToUpperInvariant());
        if (!_registrationKeys.Add(key))
        {
            return RegistrationOutcome.Duplicate;
        }

        _registrations.Add(new EventRegistration
        {
            Id = registration.Id,
            EventId = registration.EventId,
            Name = registration.Name.Trim(),
            Email = normalizedEmail,
            RegisteredAtUtc = registration.RegisteredAtUtc
        });

        Changed?.Invoke();

        return RegistrationOutcome.Added;
    }
}
