using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Shoppable.Models;

public class ApplicationUser : IdentityUser// this appuser's id is string and generated as Guid, if you want an integer id let appuser iherits from IdentityUser<int>
{

    [MinLength(2, ErrorMessage = "Name must be greater than 2 characters")]
    [MaxLength(50, ErrorMessage = "Name must be less than 50 characters")]
    public string Name { get; set; }

    public Merchant merchant { get; set; }
    public Customer customer { get; set; }



}