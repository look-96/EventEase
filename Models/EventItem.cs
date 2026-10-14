using System.ComponentModel.DataAnnotations;

namespace EventEase.Models;

public sealed class EventItem
{
    public int Id { get; init; }
    [Required(ErrorMessage = "Event name is required.")]
    [StringLength(120, ErrorMessage = "Event name must be 120 characters or fewer.")]
    public string Name { get; init; } = string.Empty;

    [Range(typeof(DateTime), "2000-01-01", "2100-12-31", ErrorMessage = "Enter a valid event date.")]
    public DateTime Date { get; init; }

    [Required(ErrorMessage = "Event location is required.")]
    [StringLength(160, ErrorMessage = "Event location must be 160 characters or fewer.")]
    public string Location { get; init; } = string.Empty;

    [StringLength(1000, ErrorMessage = "Description must be 1,000 characters or fewer.")]
    public string Description { get; init; } = string.Empty;
}
