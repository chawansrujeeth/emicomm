using System;
using System.Collections.Generic;

namespace EmiCommerce.Models;

public partial class UserProfile
{
    public Guid Id { get; set; }

    public Guid? UserId { get; set; }

    public string? FullName { get; set; }

    public string? MobileNumber { get; set; }

    public DateOnly? Dob { get; set; }

    public virtual User? User { get; set; }
}
