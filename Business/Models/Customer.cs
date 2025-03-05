using System.ComponentModel.DataAnnotations.Schema;

namespace Business.Models;

public class Customer
{
    public int Id { get; set; }

    [Column(TypeName = "nvarchar(150)")]
    public string CustomerName { get; set; } = null!;
}
