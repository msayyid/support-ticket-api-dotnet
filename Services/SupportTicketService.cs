using Microsoft.AspNetCore.Http.HttpResults;

namespace SupportTicketApi.Services;

public class SupportTicketService : ISupportTicketService
{

    private readonly List<SupportTicket> _tickets =
    [
        new(1, "Cannot log in", "Account", false),
        new(2, "Payment charged twice", "Billing", false),
        new(3, "App crashes", "Technical", true),
        new(4, "General question", "Other", false)
    ];
    
    public List<SupportTicket> GetAll(string? category, bool? isClosed)
    {
        if (category is not null && isClosed is not null)
        {
            return _tickets.Where(ticket => ticket.Category == category && ticket.IsClosed == isClosed).ToList();
        }

        if (category is not null)
        {
            return _tickets.Where(ticket => ticket.Category == category).ToList();
        }

        if (isClosed is not null)
        {
            return _tickets.Where(ticket => ticket.IsClosed == isClosed).ToList();
        }
        
        return _tickets;
    }

    public SupportTicket? GetById(int id)
    {
        return _tickets.FirstOrDefault(ticket => ticket.Id == id);
    }

    public SupportTicket Create(SupportTicket newTicket)
    {
        _tickets.Add(newTicket);
        return newTicket;
    }

    public SupportTicket? Update(int id, SupportTicket updatedTicket)
    {
        SupportTicket? ticket = _tickets.FirstOrDefault(ticket => ticket.Id == id);
        if (ticket is null)
        {
            return null;
        }

        int index = _tickets.IndexOf(ticket);
        SupportTicket replacement = updatedTicket with { Id = id };
        _tickets[index] = replacement;
        return replacement;
    }


    public SupportTicket? UpdateStatus(int id, bool isClosed)
    {
        SupportTicket? ticket = _tickets.FirstOrDefault(ticket => ticket.Id == id);
        if (ticket is null)
        {
            return null;
        }

        int index = _tickets.IndexOf(ticket);
        SupportTicket updatedTicket = ticket with { IsClosed = isClosed };

        _tickets[index] = updatedTicket;
        return updatedTicket;
    }


    public bool Delete(int id)
    {
        SupportTicket? ticket = _tickets.FirstOrDefault(ticket => ticket.Id == id);
        if (ticket is null)
        {
            return false;
        }

        _tickets.Remove(ticket);
        return true;

    }
}