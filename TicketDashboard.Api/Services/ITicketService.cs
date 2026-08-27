using Microsoft.EntityFrameworkCore;
using TicketDashboard.Api.Models;
using TicketDashboard.Api.Dtos;


public interface ITicketService{

    Task<Ticket?> GetTicketById(int id); //adding interface to be used by by TicketService

    Task<List<Ticket>> GetTickets();

    Task<Ticket> CreateTicket(CreateTicketDto dto);

    Task<Ticket?> UpdateTicket(int id, UpdateTicketDto dto);

    Task<bool> DeleteTicket(int id);

}