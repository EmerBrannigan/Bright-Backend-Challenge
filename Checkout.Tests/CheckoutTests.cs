using Checkout;

using Checkout;

namespace Checkout.Tests;

public class CheckoutTests
{

    private static Checkout CreateCheckout()
    {
        var rules = new[]
        {
            new PricingRule("A", 50, 3, 130),
            new PricingRule("B", 30, 2, 45),
            new PricingRule("C", 20),
            new PricingRule("D", 15)
        };

        return new Checkout(rules);
    }


    [Fact]
    public void Scan_SingleItem_ReturnsUnitPrice()
    {
        var checkout = CreateCheckout();

        checkout.Scan("A");

        var total = checkout.GetTotalPrice();

        Assert.Equal(50, total);
    }

    [Fact]
    public void Scan_MultipleItemsWithoutSpecial_PricesIndividually()
    {
        var checkout = CreateCheckout();

        checkout.Scan("A");
        checkout.Scan("A");

        var total = checkout.GetTotalPrice();

        Assert.Equal(100, total);
    }

    [Fact]
    public void Scan_ItemsQualifyingForSpecial_AppliesSpecialPrice()
    {
        var checkout = CreateCheckout();

        checkout.Scan("A");
        checkout.Scan("A");
        checkout.Scan("A");

        var total = checkout.GetTotalPrice();

        Assert.Equal(130, total);
    }

    [Fact]
    public void Scan_ItemsQualifyingForSpecialMultipleTimes_AppliesSpecialRepeatedly()
    {
        var checkout = CreateCheckout();

        for (int i = 0; i < 6; i++)
        {
            checkout.Scan("A");
        }

        var total = checkout.GetTotalPrice();

        Assert.Equal(260, total);
    }

    [Fact]
    public void Scan_MixedItemsInAnyOrder_AppliesCorrectTotal()
    {
        var checkout = CreateCheckout();

        checkout.Scan("B");
        checkout.Scan("A");
        checkout.Scan("B");

        var total = checkout.GetTotalPrice();

        Assert.Equal(95, total);
    }

    [Fact]
    public void Scan_ItemsWithoutSpecial_PricesCorrectly()
    {
        var checkout = CreateCheckout();

        checkout.Scan("C");
        checkout.Scan("D");

        var total = checkout.GetTotalPrice();

        Assert.Equal(35, total);
    }

}