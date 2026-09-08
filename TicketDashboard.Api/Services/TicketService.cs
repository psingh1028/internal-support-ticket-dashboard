using Microsoft.EntityFrameworkCore;
using TicketDashboard.Api.Models;
using TicketDashboard.Api.Dtos;
public class TicketService: ITicketService
{
    private readonly AppDbContext _context;
    
    public TicketService(AppDbContext context) // adding service layer for project
    {
        _context = context;
    }
    
    public async Task<Ticket?> GetTicketById(int id){  //this methods search logic is being handled here now 

        var ticket = await _context.Tickets.FirstOrDefaultAsync(t => t.Id == id); 

        return ticket;

    }

    public async Task<List<Ticket>> GetTickets(){ // logic for this method is also being handled by service layer

        List<Ticket> tickets = await _context.Tickets.ToListAsync();

        return tickets;
    }

    public async Task<Ticket> CreateTicket(CreateTicketDto dto) // logic for createTicket method handling transfer from dto to created ticket
    {
        var ticket = new Ticket{
        Title = dto.Title,
        Description = dto.Description,
        Status = dto.Status,
        Priority = dto.Priority,
        CreatedAt = DateTime.UtcNow
     };

        _context.Tickets.Add(ticket);
        await _context.SaveChangesAsync();

        return ticket;
    }


    public async Task<Ticket?> UpdateTicket(int id, UpdateTicketDto dto) // logic for updateticket handling transfer of dto data to ticket object
    {
        var ticket = await GetTicketById(id);

        if (ticket == null)
            return null;

        ticket.Title = dto.Title;
        ticket.Description = dto.Description;
        ticket.Status = dto.Status;
        ticket.Priority = dto.Priority;
        //ticket.CreatedAt = DateTime.UtcNow; to be deleted but kept for future reference to error 

        await _context.SaveChangesAsync();

        return ticket;
    }

    public async Task<bool> DeleteTicket(int id) // logic for delete ticket moved to service layer
    {
        var ticket = await _context.Tickets.FirstOrDefaultAsync(t => t.Id == id);

        if (ticket == null)
            return false;

        _context.Tickets.Remove(ticket);
        await _context.SaveChangesAsync();
        
        return true;
    }

}
