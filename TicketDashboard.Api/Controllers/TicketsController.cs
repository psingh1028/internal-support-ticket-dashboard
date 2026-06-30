using Microsoft.AspNetCore.Mvc;
using TicketDashboard.Api.Models;

namespace TicketDashboard.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketsController : ControllerBase
{
    private static int nextId = 5;

      private static List<Ticket> tickets = new List<Ticket>
      { 
            new Ticket
            {
                Id = 1,
                Title = "Customer unable to access online banking",
                Description = "Customer reports login error when trying to access account.",
                Status = "Open",
                Priority = "High"
            },
            new Ticket
            {
                Id = 2,
                Title = "Debit card replacement request",
                Description = "Customer needs a replacement card due to damage.",
                Status = "In Progress",
                Priority = "Medium"
            },
            new Ticket
            {
                Id = 3,
                Title = "Address change verification",
                Description = "Customer submitted address update requiring review.",
                Status = "Resolved",
                Priority = "Low"
            },
            new Ticket
            {
                Id =4,
                Title ="text too small on mobile app",
                Description = "Customer reports that text is too small to read on mobile app.",
                Status = "Open",
                Priority = "Low"
            }
        };

    [HttpGet]
    public ActionResult<IEnumerable<Ticket>> GetTickets()
    {
        return Ok(tickets);
    }

    [HttpGet("{id}")]
    public ActionResult<Ticket> GetTicketById(int id)
    {
        var ticket = tickets.FirstOrDefault(t => t.Id == id);

        if(ticket == null)
        {
            return NotFound();
        }
        return Ok(ticket);
    }

    [HttpPost]
    public ActionResult<Ticket> CreateTicket(Ticket newTicket)
    {
        newTicket.Id = nextId++;
        tickets.Add(newTicket);
        return Ok(newTicket);
    }

    [HttpPost("{id}")]
    public ActionResult<Ticket> UpdateTicket(int id, Ticket updatedTicket)
    {
        var ticket = tickets.FirstOrDefault(t => t.Id == id);

        if (ticket == null)
        {
            return NotFound();
        }

        ticket.Title = updatedTicket.Title;
        ticket.Description = updatedTicket.Description;
        ticket.Status = updatedTicket.Status;
        ticket.Priority = updatedTicket.Priority;

        return Ok(ticket);
    }

    [HttpDelete("{id}")]
    public ActionResult<Ticket> DeleteTicket(int id)
    {
        var ticket = tickets.FirstOrDefault(t => t.Id == id);

        if (ticket == null)
        {
            return NotFound();
        }

        tickets.Remove(ticket);

        return NoContent();
    }


}