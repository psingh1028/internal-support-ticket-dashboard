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
    private readonly AppDbContext _context;

    public TicketsController(AppDbContext context)
    {
        _context = context;

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
        var tickets = await _context.Tickets.ToListAsync();
        return Ok(tickets);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Ticket>> GetTicketById(int id)
    {
        var ticket = await _context.Tickets.FirstOrDefaultAsync(t => t.Id == id);

        if(ticket == null)
        {
            return NotFound();
        }
        return Ok(ticket);
    }

    [HttpPost]
    public async Task<ActionResult<Ticket>> CreateTicket(CreateTicketDto dtoTicket)
    {

        Ticket newTicket = new Ticket {
        Title = dtoTicket.Title,
        Description = dtoTicket.Description,
        Status = dtoTicket.Status,
        Priority = dtoTicket.Priority,
        CreatedAt = DateTime.UtcNow
        };

        _context.Tickets.Add(newTicket);
        await _context.SaveChangesAsync();
        return CreatedAtAction(
          nameof(GetTicketById),
          new { id = newTicket.Id },
          newTicket
        );
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Ticket>> UpdateTicket(int id, UpdateTicketDto updatedTicket)
    {
        var ticket = await _context.Tickets.FirstOrDefaultAsync(t => t.Id == id);

        if (ticket == null)
        {
            return NotFound();
        }
        if (string.IsNullOrWhiteSpace(updatedTicket.Title))
        {
            return BadRequest("Missing Title");
        }
        if (string.IsNullOrWhiteSpace(updatedTicket.Description))
        { 
            return BadRequest("Missing Description");
        }
        if (string.IsNullOrWhiteSpace(updatedTicket.Status))
        {
            return BadRequest("Missing Status");
        }
        if (string.IsNullOrWhiteSpace(updatedTicket.Priority))
        {
            return BadRequest("Missing Priority");
        }
        
            ticket.Title = updatedTicket.Title;
            ticket.Description = updatedTicket.Description;
            ticket.Status = updatedTicket.Status;
            ticket.Priority = updatedTicket.Priority;




        await _context.SaveChangesAsync();
        return Ok(ticket);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTicket(int id)
    {
        var ticket = await _context.Tickets.FirstOrDefaultAsync(t => t.Id == id);

        if (ticket == null)
        {
            return NotFound();
        }

        _context.Tickets.Remove(ticket);
        await _context.SaveChangesAsync();

        return NoContent();
    }


}