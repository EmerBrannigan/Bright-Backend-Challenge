using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.Marshalling;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Linq;

namespace Checkout;

public class Checkout : ICheckout
{
    private readonly Dictionary<string, PricingRates> _pricingRates;
    private readonly Dictionary<string, int> _scannedItems;

    public Checkout(IEnumerable<PricingRule> pricingRules)
    {
        _pricingRates = pricingRules.ToDictionary(r => r.Sku,
            r => new PricingRates(r.Sku, r.UnitPrice, r.Quantity, r.SpecialPrice));
        _scannedItems = new Dictionary<string, int>();
    }
    public void Scan(string item)
    {
        if (_scannedItems.ContainsKey(item))
        {
            _scannedItems[item]++;
        }
        else
        {
            _scannedItems[item] = 1;
        }
    }

    public int GetTotalPrice()
    {
        int totalPrice = 0;

        foreach (var scannedItem in _scannedItems)
        {
            var sku = scannedItem.Key;
            var quantity = scannedItem.Value;

            if (_pricingRates.ContainsKey(sku))
            {
                var pricingRate = _pricingRates[sku];

                if (pricingRate.Quantity.HasValue && pricingRate.SpecialPrice.HasValue)
                {
                    int specialPriceBundles = quantity / pricingRate.Quantity.Value;
                    int remainingItems = quantity % pricingRate.Quantity.Value;

                    totalPrice += specialPriceBundles * pricingRate.SpecialPrice.Value;
                    totalPrice += remainingItems * pricingRate.UnitPrice;
                }
                else
                {
                    totalPrice += quantity * pricingRate.UnitPrice;
                }
            }
        }

        return totalPrice;
    }

}
