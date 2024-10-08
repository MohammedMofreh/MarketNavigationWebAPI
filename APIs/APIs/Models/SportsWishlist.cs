﻿using System;
using System.Collections.Generic;

namespace APIs.Models;

public partial class SportsWishlist
{
    public string Email { get; set; } = null!;

    public int ProductId { get; set; }

    public string? Comment { get; set; }

    public virtual Buyer EmailNavigation { get; set; } = null!;

    public virtual SportsProduct Product { get; set; } = null!;
}
