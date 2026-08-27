using Xunit;
using Microsoft.EntityFrameworkCore;
using TicketDashboard.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using TicketDashboard.Api.Models;
using TicketDashboard.Api.Dtos;

namespace TicketDashboard.Tests;

public class TicketDashboardTests
{
    [Fact]
    public async Task GetTicketById_ReturnsNotFound_WhenTicketDoesntExist()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase("TicketTestDatabase").Options;

        var context = new AppDbContext(options);

        var service = new TicketService(context); //creating new TestTicketObject
        var controller = new TicketsController(service); // replacing context with TestTicketService object

        var result = await controller.GetTicketById(999);

        Assert.IsType<NotFoundResult>(result.Result);

    }

    [Fact]
    public async Task GetTicketById_ReturnsTicket_WhenTicketExists()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase("TicketTestDatabase").Options;

        var context = new AppDbContext(options);

        var ticket = new Ticket
        {
            Id = 6,
            Title = "Test ticket",
            Description = "Testing GetTicketById",
            Status = "Open",
            Priority = "High"

        };

        context.Tickets.Add(ticket);
        await context.SaveChangesAsync();

        var service = new TicketService(context); //creating new TestTicketObject
        var controller = new TicketsController(service); // replacing context with TestTicketService object

        var result = await controller.GetTicketById(ticket.Id);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);

        var returnedTicket = Assert.IsType<Ticket>(okResult.Value);

        Assert.Equal(ticket.Id, returnedTicket.Id);
        Assert.Equal("Test ticket", returnedTicket.Title);
        Assert.Equal(ticket.Status, returnedTicket.Status);

    }

    [Fact]
        public async Task CreateTicket_ReturnCreatedTicket()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase("CreateTicketTestDatabase").Options;

        var context = new AppDbContext(options);

        var service = new TicketService(context); //creating new TestTicketObject
        var controller = new TicketsController(service); // replacing context with TestTicketService object

        var ticket = new CreateTicketDto
        {
            Title = "Printer issue",
            Description = "Printer is not connecting",
            Status = "Open",
            Priority = "High"

        };

        var result = await controller.CreateTicket(ticket);

        var okResult = Assert.IsType<CreatedAtActionResult>(result.Result);

        var rtnTicket = Assert.IsType<Ticket>(okResult.Value);

        Assert.True(rtnTicket.Id>0);
        Assert.Equal(ticket.Title, rtnTicket.Title);
        Assert.Equal(ticket.Description, rtnTicket.Description);
        Assert.Equal(ticket.Status, rtnTicket.Status);
        Assert.Equal(ticket.Priority, rtnTicket.Priority);
    }

    [Fact]
    public async Task DeleteTicket_ReturnNotFound_WhenTicketDoesNotExist()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase("DeleteTicketDatabase").Options;

        var context = new AppDbContext(options);

        var service = new TicketService(context); //creating new TestTicketObject
        var controller = new TicketsController(service); // replacing context with TestTicketService object

        var result = await controller.DeleteTicket(1);

        Assert.IsType<NotFoundResult>(result);


    }

    [Fact]
    public async Task DeleteTicket_ReturnSuccessfulDelete()
    {
        //arrange
        var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase("DeleteTicketDatabase2").Options;

        var context = new AppDbContext(options);

        var service = new TicketService(context); //creating new TestTicketObject
        var controller = new TicketsController(service); // replacing context with TestTicketService object

        //assign
        var ticket = new Ticket
        {
            Id = 1,
            Title = "delete test",
            Description = "delete is connecting",
            Status = "Open",
            Priority = "High"

        };

        context.Tickets.Add(ticket);
        await context.SaveChangesAsync();

        var result = await controller.DeleteTicket(1);

        //assert
        Assert.IsType<NoContentResult>(result);

        var deletedTicket = await context.Tickets.FindAsync(ticket.Id);

        Assert.Null(deletedTicket);


    }

    // [Fact]
    // public async Task GetTicketById_WhenTicketDoesNotExist_ReturnsNotFound(){
    //     // Arrange
    //     var service = new TestTicketService();
    //     var controller = new TicketsController(service);

    //     // Act
    //     var result = await controller.GetTicketById(999);

    //     // Assert
    //     Assert.IsType<NotFoundResult>(result.Result);

    // }

    [Fact]
    public async Task GetTicketById_WhenTicketExists_ReturnsValidTicket(){
         //arrange
         var service = new TestTicketService();
         var controller = new TicketsController(service);
        
        //act
         var result = await controller.GetTicketById(47);

        //assert
         var okResult = Assert.IsType<OkObjectResult>(result.Result);
         Assert.IsType<Ticket>(okResult.Value); 



    }





}

