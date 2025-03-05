using System.ComponentModel.DataAnnotations.Schema;

namespace Business.Models;

public class Project
{
    public int Id { get; set; }

    [Column(TypeName = "nvarchar(150)")]
    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    [Column(TypeName = "date")]
    public DateTime StartDate { get; set; }

    [Column(TypeName = "date")]
    public DateTime EndDate { get; set; }
}
