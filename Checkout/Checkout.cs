namespace Checkout;

public class Checkout : ICheckout
{
    private readonly Dictionary<string, PricingRates> _pricingRates;
    private readonly Dictionary<string, int> _scannedItems;

    public Checkout(Dictionary<string, PricingRates> pricingRates)
    {
        _pricingRates = pricingRates;
        _scannedItems = new Dictionary<string, int>();
    }
}
