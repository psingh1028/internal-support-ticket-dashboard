using Microsoft.AspNetCore.Mvc;
using TicketDashboard.Api.Models;

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
    public ActionResult<IEnumerable<Ticket>> GetTickets()
    {
        return Ok(_context.Tickets.ToList());
    }

    [HttpGet("{id}")]
    public ActionResult<Ticket> GetTicketById(int id)
    {
        var ticket = _context.Tickets.FirstOrDefault(t => t.Id == id);

        if(ticket == null)
        {
            return NotFound();
        }
        return Ok(ticket);
    }

    [HttpPost]
    public ActionResult<Ticket> CreateTicket(Ticket newTicket)
    {
        // newTicket.Id = nextId++;
        // tickets.Add(newTicket);
        _context.Tickets.Add(newTicket);
        _context.SaveChanges();
        return CreatedAtAction(
          nameof(GetTicketById),
          new { id = newTicket.Id },
          newTicket
        );
    }

    [HttpPut("{id}")]
    public ActionResult<Ticket> UpdateTicket(int id, Ticket updatedTicket)
    {
        var ticket = _context.Tickets.FirstOrDefault(t => t.Id == id);

        if (ticket == null)
        {
            return NotFound();
        }
        if (updatedTicket.Title == null || updatedTicket.Title == "")
        {
            return BadRequest("Missing Title");
        }
        if (updatedTicket.Description == null || updatedTicket.Description == "")
        {
            return BadRequest("Missing Description");
        }
        if (updatedTicket.Status == null || updatedTicket.Status == "")
        {
            return BadRequest("Missing Status");
        }
        if (updatedTicket.Priority == null || updatedTicket.Priority == "")
        {
            return BadRequest("Missing Priority");
        }
        
            ticket.Title = updatedTicket.Title;
            ticket.Description = updatedTicket.Description;
            ticket.Status = updatedTicket.Status;
            ticket.Priority = updatedTicket.Priority;



        _context.SaveChanges();
        return Ok(ticket);
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteTicket(int id)
    {
        var ticket = _context.Tickets.FirstOrDefault(t => t.Id == id);

        if (ticket == null)
        {
            return NotFound();
        }

        _context.Tickets.Remove(ticket);
        _context.SaveChanges();

        return NoContent();
    }


}