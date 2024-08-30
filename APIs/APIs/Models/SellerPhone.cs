using System;
using System.Collections.Generic;

namespace APIs.Models;

public partial class SellerPhone
{
    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public virtual Seller EmailNavigation { get; set; } = null!;
}
