using EventEase.Models;

namespace EventEase.Data;

public static class EventCatalog
{
    private static readonly IReadOnlyList<EventItem> Events =
    [
        new EventItem
        {
            Id = 1,
            Name = "Future of Work Summit",
            Date = new DateTime(2027, 2, 18),
            Location = "London, UK",
            Description = "A day of talks and workshops on the changing world of work."
        },
        new EventItem
        {
            Id = 2,
            Name = "Spring Charity Gala",
            Date = new DateTime(2027, 4, 10),
            Location = "Manchester, UK",
            Description = "An evening bringing the community together to support local causes."
        },
        new EventItem
        {
            Id = 3,
            Name = "Digital Marketing Forum",
            Date = new DateTime(2027, 6, 3),
            Location = "Birmingham, UK",
            Description = "Practical sessions on customer experience, analytics, and digital growth."
        }
    ];

    private static readonly IReadOnlyDictionary<int, EventItem> EventsById =
        Events.ToDictionary(eventItem => eventItem.Id);

    public static IReadOnlyList<EventItem> GetAll() => Events;

    public static EventItem? GetById(int id) =>
        EventsById.TryGetValue(id, out var eventItem) ? eventItem : null;
}
