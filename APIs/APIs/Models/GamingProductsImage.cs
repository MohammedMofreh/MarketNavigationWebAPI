using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APIs.Models;

public partial class GamingProductsImage
{
    
    public int ProductImageId { get; set; }
    public int? ProductId { get; set; }
    
    public byte[] ProductImage { get; set; }

    public virtual GamingProduct? Product { get; set; }
}
