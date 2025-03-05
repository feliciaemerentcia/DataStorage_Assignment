using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Business.Dtos;

public class CustomerRegistrationForm
{
    [Required]
    public string CustomerName { get; set; } = null!;
}
