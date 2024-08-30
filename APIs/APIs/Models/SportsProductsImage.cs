﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIs.Models;

public partial class SportsProductsImage
{
    
    public int ProductImageId { get; set; }
    public int? ProductId { get; set; }
    
    public byte[] ProductImage { get; set; }

    public virtual SportsProduct? Product { get; set; }
}
