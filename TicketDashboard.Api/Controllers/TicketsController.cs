using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketDashboard.Api.Models;
using TicketDashboard.Api.Dtos;

namespace TicketDashboard.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketsController : ControllerBase
{
   // private static int nextId = 5; //left for syntax reference only
    private readonly TicketService _ticketService; // replacing app db context to ticketserive using Dependency injection

    public TicketsController(TicketService service) //now takes in TicketService object
    {
        _ticketService = service;

    }
    // old list left for syntax reference only
     /* private static List<Ticket> tickets = new List<Ticket>
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
        };*/

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Ticket>>> GetTickets()
    {
        var tickets = await _ticketService.GetAllTickets(); // this logic is handled by service layer now
        return Ok(tickets);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Ticket>> GetTicketById(int id)
    {
        var ticket = await _ticketService.GetTicketById(id); // now this logic is handled by service layer

        if(ticket == null)
        {
            return NotFound();
        }
        return Ok(ticket);
    }

    [HttpPost]
    public async Task<ActionResult<Ticket>> CreateTicket(CreateTicketDto dtoTicket)
    {

        Ticket newTicket = await _ticketService.CreateTicket(dtoTicket);
        return CreatedAtAction(
          nameof(GetTicketById),
          new { id = newTicket.Id },
          newTicket
        );
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Ticket>> UpdateTicket(int id, UpdateTicketDto updatedTicket)
    {
        var ticket = await _ticketService.UpdateTicket(id,updatedTicket);

         if (ticket == null)
        {
            return NotFound();
        }
        
        return Ok(ticket);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTicket(int id)
    {
        var ifDeleted = await _ticketService.DeleteTicket(id);

        if (!ifDeleted)
        {
            return NotFound();
        }


        return NoContent();
    }


}