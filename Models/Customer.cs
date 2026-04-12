namespace MiniERP.Models;
using System.ComponentModel.DataAnnotations;

public class Customer
{
    [Key]
    public string PhoneNumber { get; set; } = "";
    public string Name { get; set; } = "";
}