namespace SupportTicketApi.Services;

public interface ISupportTicketService
{
    List<SupportTicket> GetAll(string? category, bool? isClosed);

    SupportTicket? GetById(int id);
    
    SupportTicket Create(SupportTicket newTicket);
    
    SupportTicket? Update(int id, SupportTicket updatedTicket);

    SupportTicket? UpdateStatus(int id, bool isClosed);

    bool Delete(int id);
}