using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows.Media;

namespace JumperCompany.Models.Entities;

public partial class Agent
{
    public int AgentId { get; set; }

    public int AgentTypeId { get; set; }

    public string AgentName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string? AgentLogo { get; set; }

    public string Address { get; set; } = null!;

    public int Priority { get; set; }

    public int DirectorId { get; set; }

    public string Tin { get; set; } = null!;

    public string Kpp { get; set; } = null!;

    public virtual AgentType AgentType { get; set; } = null!;

    public virtual Director Director { get; set; } = null!;

    public virtual ICollection<MinAgentPriceHistory> MinAgentPriceHistories { get; set; } = new List<MinAgentPriceHistory>();

    public virtual ICollection<ProductSale> ProductSales { get; set; } = new List<ProductSale>();    

    [NotMapped]
    public string AgentLogoPath => "/Resources" + AgentLogo;

    [NotMapped]
    public int AgentSalesForYear => CountSalesForYear();

    [NotMapped]
    public int AgentDiscountAmount => CountAgentDiscount();

    [NotMapped]
    public SolidColorBrush AgentBackgroundDiscount => AgentDiscountAmount >= 25 ? new SolidColorBrush(Colors.LightGreen) : new SolidColorBrush(Colors.White);

    public int CountSalesForYear()
    {
        var countSales = 0;

        foreach (var item in ProductSales)
        {
            if (item.RealizeDate.AddDays(365) >= DateTime.Now)
            {
                countSales += item.AmountOfProducts;
            }
        }

        return countSales;
    }

    public int CountAgentDiscount()
    {
        decimal countProducts = 0;
        foreach (var item in ProductSales)
        {
            countProducts += item.AmountOfProducts * item.Product.MinPriceForAgent;
        }

        
        if (countProducts <= 10000)
        {
            return 0;
        }
        if (countProducts <= 50000)
        {
            return 5;
        }
        if (countProducts <= 150000)
        {
            return 10;
        }
        if (countProducts <= 500000)
        {
            return 20;
        }
        return 25;
    }
}
