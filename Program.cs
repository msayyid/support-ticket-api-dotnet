// Support Ticket API

using SupportTicketApi.Services;

var builder = WebApplication.CreateBuilder(args);

// enable controllers
builder.Services.AddControllers();

// DI registration
builder.Services.AddSingleton<ISupportTicketService, SupportTicketService>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
// enable controllers (routing)
app.MapControllers();

app.Run();


// The architecture should be:
// HTTP
//     ↓
// SupportTicketsController
//     ↓
// ISupportTicketService
//     ↓
// SupportTicketService
//     ↓
// List<SupportTicket>