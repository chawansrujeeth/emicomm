using System;
using System.Collections.Generic;

namespace EmiCommerce.Models;

public partial class UserProfile
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string? FullName { get; set; }

    public string? MobileNumber { get; set; }

    public DateOnly? Dob { get; set; }

    public virtual User? User { get; set; }
}
