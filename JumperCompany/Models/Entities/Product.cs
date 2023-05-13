using System;
using System.Collections.Generic;

namespace JumperCompany.Models.Entities;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public int ProductTypeId { get; set; }

    public string ProductArticle { get; set; } = null!;

    public int NumberOfManufacturers { get; set; }

    public int WorkshopNumber { get; set; }

    public decimal MinPriceForAgent { get; set; }

    public virtual ICollection<ProductSale> ProductSales { get; set; } = new List<ProductSale>();

    public virtual ProductType ProductType { get; set; } = null!;
}
