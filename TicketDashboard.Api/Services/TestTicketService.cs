using Microsoft.EntityFrameworkCore;
using TicketDashboard.Api.Models;
using TicketDashboard.Api.Dtos;

public class TestTicketService : ITicketService{

    // public Task<Ticket?> GetTicketById(int id){ //this is used for unit testing when getTicketById returns null

    //     return Task.FromResult<Ticket?>(null);
    // }

    public Task<Ticket?> GetTicketById(int id){ // this method is used for unit testing when getTicketById returns a valid ticket
        
         Ticket ticket = new Ticket{
            Id = id,
            Title = "exists test",
            Description = "GetTicketById Unit Test",
            Status = "open",
            Priority = "medium",
            CreatedAt = DateTime.UtcNow,
        };

        return Task.FromResult<Ticket?>(ticket);
    }

    public Task<List<Ticket>> GetTickets(){

        throw new NotImplementedException();
    }

    public Task<Ticket> CreateTicket(CreateTicketDto dto){
        
        throw new NotImplementedException();

    }

    public Task<Ticket?> UpdateTicket(int id, UpdateTicketDto dto){

        throw new NotImplementedException();
    }

    public Task<bool> DeleteTicket(int id){

        throw new NotImplementedException();
    }
}