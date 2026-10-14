# EventEase

EventEase is a Blazor WebAssembly sample application for discovering events and managing attendee registration and check-in. It is built as a client-side demonstration with mock event data and in-memory state.

## Features

- Browse events in a virtualized list
- View event name, date, location, and description
- Navigate between event list, event details, registration, and My Registrations
- Validate attendee name and email before accepting a registration
- Reject duplicate registrations for the same event email address
- Track the active browser session's attendee name and registration count
- Check in registered attendees once and show check-in time and event totals
- Show friendly recovery links for unknown routes and event IDs
- Responsive form, registration, and empty-state layouts

## Requirements

- .NET 9 SDK
- An editor such as Visual Studio Code (optional)

## Run locally

Open a terminal in the directory containing `EventEase.csproj` and run:

```bash
dotnet restore
dotnet run
```

Open the local URL printed by the command. The main routes are:

| Page | Route |
| --- | --- |
| Home | `/` |
| Browse events | `/events` |
| Event details | `/events/{eventId}` |
| Registration | `/events/{eventId}/register` |
| My registrations | `/my-registrations` |

## Typical user flow

1. Choose **Browse events** to see the available events.
2. Open an event's details, then choose **Register**.
3. Enter a name and a valid email address. Required fields, maximum lengths, and email format are checked before the form is accepted.
4. The app rejects a second registration for the same event using the same email address, ignoring email letter case.
5. Open **My registrations** to review sign-ups and check in an attendee. A registration can only be checked in once.

## State and data behavior

`RegistrationState`, `UserSessionTracker`, and `AttendanceTracker` are registered as scoped services in `Program.cs`. In Blazor WebAssembly, the services keep state while the app remains loaded and the user moves between routes. This sample does not persist data: reloading or closing the page clears registrations, session details, and check-ins.

`EventCatalog` supplies the sample events. `EventItem` and `EventRegistration` define the event and registration data models. Registration validation is applied in the form and checked again in `RegistrationState` before accepting data. Attendance records refer to a registration ID and are accepted only for an existing registration.

## Project structure

```text
Data/                 Mock event catalog
Layout/               Shared page layout and navigation
Models/               Event and registration models
Pages/                Event list, details, registration, and registration summary
Services/             Registration, session, and attendance state
Shared/                Reusable EventCard component
wwwroot/               Static assets and application styles
App.razor              Blazor router and not-found view
Program.cs             WebAssembly startup and dependency injection
```

## Build and publish

Build the project:

```bash
dotnet build -c Release
```

Publish the static web app:

```bash
dotnet publish -c Release
```

The deployable site files are generated in:

```text
bin/Release/net9.0/publish/wwwroot
```

Upload the **contents** of this `wwwroot` directory to a static web host. Configure the host to return `index.html` for client-side routes such as `/events/1` and `/my-registrations` (an SPA fallback or rewrite rule). Without that host setting, directly refreshing a nested route may return a host-level 404 even though in-app navigation works.

## Accessibility notes

The app uses associated form labels, Blazor validation messages, status and alert roles for feedback, semantic page sections, and keyboard-operable links and buttons. Check color contrast and keyboard behavior again if you change the theme or controls.

## Dependencies

Direct NuGet dependencies are:

- `Microsoft.AspNetCore.Components.WebAssembly` — Blazor WebAssembly runtime and components
- `Microsoft.AspNetCore.Components.WebAssembly.DevServer` — local development server; marked as private assets so it is not a runtime dependency for deployed users

## Production considerations

This project is a front-end sample, not a production registration system. Before using it for real attendees, connect the state services to an authenticated API and durable database, enforce duplicate and attendance rules on the server, protect personal information, and configure the production host's routing and HTTPS settings. The in-memory client state is visible to the current browser user and must not be treated as secure storage.
