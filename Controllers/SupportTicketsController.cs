using Microsoft.AspNetCore.Mvc;
using SupportTicketApi.Services;

namespace SupportTicketApi.Controllers;

[ApiController]
[Route("tickets")]
public class SupportTicketsController : ControllerBase
{
    private readonly ISupportTicketService _service;

    public SupportTicketsController(ISupportTicketService service)
    {
        _service = service;
    }
    
    [HttpGet]
    public IActionResult GetAllTickets(string? category, bool? isClosed)
    {
        List<SupportTicket> tickets = _service.GetAll(category, isClosed);
        return Ok(tickets);
    }
    
    
    [HttpGet("{id}")]
    public IActionResult GetTicketById(int id)
    {
        SupportTicket? ticket = _service.GetById(id);
        if (ticket is null)
        {
            return NotFound();
        }
        return Ok(ticket);
    }


    private static readonly string[] validCategories =
    [
        "Technical",
        "Billing",
        "Account",
        "Other"
    ];
    [HttpPost]
    public IActionResult CreateTicket(SupportTicket newTicket)
    {
        if (string.IsNullOrWhiteSpace(newTicket.Title))
        {
            return BadRequest("Title Cannot be empty");
        }

        if (string.IsNullOrWhiteSpace(newTicket.Category))
        {
            return BadRequest("Category cannot be empty");
        }

        if (!validCategories.Contains(newTicket.Category))
        {
            return BadRequest("Invalid category");
        }

        SupportTicket createdTicket = _service.Create(newTicket);
        return CreatedAtAction(
            nameof(GetTicketById), // which action retrieves it
            new { id = createdTicket.Id }, // what goes into {id}
            createdTicket // what object goes in response body
            );
    }


    [HttpPut("{id}")]
    public IActionResult UpdateTicket(int id, SupportTicket updatedTicket)
    {
        if (string.IsNullOrWhiteSpace(updatedTicket.Title))
        {
            return BadRequest("Title cannot be empty");
        }

        if (string.IsNullOrWhiteSpace(updatedTicket.Category))
        {
            return BadRequest("Category cannot be empty");
        }

        if (!validCategories.Contains(updatedTicket.Category))
        {
            return BadRequest("Invalid category");
        }

        SupportTicket? result = _service.Update(id, updatedTicket);
        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);

    }

    [HttpPatch("{id}")]
    public IActionResult UpdateTicketStatus(int id, UpdateTicketStatusRequest request)
    {
        SupportTicket? result = _service.UpdateStatus(id, request.IsClosed);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteTicket(int id)
    {
        bool deleted = _service.Delete(id);
        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
    
    
}