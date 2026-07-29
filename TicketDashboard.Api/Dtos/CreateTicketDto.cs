using System.ComponentModel.DataAnnotations;

namespace TicketDashboard.Api.Dtos;
public class CreateTicketDto
{
    [Required]
    [StringLength(100)]
    public required string Title { get; set; }
    [Required]
    public required string Description { get; set; }

    [Required]
    public required string Status{ get; set; }
    [Required]
    public required string Priority { get; set; }
    

}
