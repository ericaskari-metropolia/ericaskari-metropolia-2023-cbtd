namespace Infrastructure.Models;

using System.ComponentModel.DataAnnotations;

public class Manufacturer
{
    [Key] public int Id { get; set; }

    [Required] public string? Name { get; set; }
}