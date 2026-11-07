# Support Ticket API

A REST API for managing support tickets, built with **C#**, **ASP.NET Core**, and **.NET 10**.

This project focuses on a cleaner backend architecture using **Controllers**, a **Service layer**, **Interfaces**, and **Dependency Injection**.

## Features

- Get all support tickets
- Get a ticket by ID
- Filter tickets by category
- Filter tickets by closed/open status
- Combine multiple filters
- Create new tickets
- Fully update existing tickets
- Update only the ticket status
- Delete tickets
- Request validation
- Appropriate HTTP status codes

## Tech Stack

- C#
- .NET 10
- ASP.NET Core
- ASP.NET Core Controllers
- Dependency Injection
- Service Layer
- Interfaces
- LINQ

## Architecture

The application follows this structure:

```text
HTTP Request
    ↓
SupportTicketsController
    ↓
ISupportTicketService
    ↓
SupportTicketService
    ↓
List<SupportTicket>
```

### Controller

The controller is responsible for:

- Handling HTTP requests
- Validating request data
- Returning HTTP status codes and responses

### Service

The service is responsible for:

- Retrieving tickets
- Creating tickets
- Updating tickets
- Updating ticket status
- Deleting tickets
- Filtering data

### Interface

`ISupportTicketService` defines the operations that a support ticket service must provide.

The controller depends on the interface rather than directly depending on the concrete service implementation.

### Dependency Injection

The service is registered in `Program.cs`:

```csharp
builder.Services.AddSingleton<ISupportTicketService, SupportTicketService>();
```

This tells ASP.NET Core to provide a `SupportTicketService` whenever an `ISupportTicketService` is requested.

## Support Ticket Model

Each ticket contains:

```json
{
  "id": 1,
  "title": "Cannot log in",
  "category": "Account",
  "isClosed": false
}
```

Supported categories:

- Technical
- Billing
- Account
- Other

## API Endpoints

| Method | Endpoint | Description |
|---|---|---|
| GET | `/tickets` | Get all tickets |
| GET | `/tickets/{id}` | Get a ticket by ID |
| POST | `/tickets` | Create a ticket |
| PUT | `/tickets/{id}` | Fully update a ticket |
| PATCH | `/tickets/{id}` | Update ticket status |
| DELETE | `/tickets/{id}` | Delete a ticket |

## Filtering

Filter by category:

```http
GET /tickets?category=Technical
```

Filter by status:

```http
GET /tickets?isClosed=false
```

Combine filters:

```http
GET /tickets?category=Billing&isClosed=false
```

## Example POST Request

```http
POST /tickets
Content-Type: application/json
```

```json
{
  "id": 5,
  "title": "Password reset not working",
  "category": "Account",
  "isClosed": false
}
```

A successful request returns:

```text
201 Created
```

along with the created ticket and a location pointing to its GET-by-ID endpoint.

## Example PUT Request

```http
PUT /tickets/2
Content-Type: application/json
```

```json
{
  "id": 999,
  "title": "Updated billing issue",
  "category": "Billing",
  "isClosed": true
}
```

The route ID is treated as authoritative, so the stored ticket keeps:

```text
id = 2
```

rather than using the ID from the request body.

## Example PATCH Request

```http
PATCH /tickets/1
Content-Type: application/json
```

```json
{
  "isClosed": true
}
```

This changes only the `isClosed` property while keeping the rest of the ticket unchanged.

## Validation

The API validates that:

- Title is not empty
- Category is not empty
- Category is one of:
  - Technical
  - Billing
  - Account
  - Other

Invalid requests return:

```text
400 Bad Request
```

Missing tickets return:

```text
404 Not Found
```

Successful deletions return:

```text
204 No Content
```

## Running the Project

Make sure the .NET SDK is installed.

Clone the repository:

```bash
git clone <repository-url>
```

Move into the project directory:

```bash
cd SupportTicketApi
```

Run the application:

```bash
dotnet run
```

The terminal will display the local URL where the API is running.

## Testing

A `requests.http` file can be used in JetBrains Rider or another compatible IDE to test the API endpoints.

## Current Limitation

The project currently uses an **in-memory list** for data storage.

This means created, updated, and deleted data is lost when the application restarts.

Database persistence will be introduced in later projects using SQL and Entity Framework Core.

## Project Purpose

This project was built as part of my C# and ASP.NET Core learning path, with a focus on:

- REST API development
- ASP.NET Core Controllers
- Service-layer architecture
- Dependency Injection
- Interfaces
- Constructor injection
- Separation of concerns
- LINQ
- CRUD operations
- Filtering
- Validation
- HTTP status handling
