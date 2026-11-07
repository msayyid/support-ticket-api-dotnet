namespace SupportTicketApi;

public record SupportTicket(
    int Id,
    string Title,
    string Category, // question should i create an enum for category? or will having just an array suffice?
    bool IsClosed
    );