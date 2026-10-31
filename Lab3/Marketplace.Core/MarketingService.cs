namespace Marketplace.Core;

public class MarketingService
{
    public void ApplySecretDiscount(ShoppingCart cart, decimal amount)
    {
        if (cart == null)
            throw new ArgumentNullException(nameof(cart));

        cart.SetDiscount(amount);
    }
}