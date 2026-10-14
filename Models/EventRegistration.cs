using System.ComponentModel.DataAnnotations;

namespace EventEase.Models;

public sealed class EventRegistration
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public int EventId { get; init; }

    [Required(ErrorMessage = "Please enter your name.")]
    [StringLength(100, ErrorMessage = "Name must be 100 characters or fewer.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter your email address.")]
    [StringLength(254, ErrorMessage = "Email address must be 254 characters or fewer.")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    public string Email { get; set; } = string.Empty;

    public DateTime RegisteredAtUtc { get; init; } = DateTime.UtcNow;
}
