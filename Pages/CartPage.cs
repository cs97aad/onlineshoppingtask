using Microsoft.Playwright;
using System.Threading.Tasks;

namespace AutomationExerciseTests.Pages
{
    public class CartPage
    {
        private readonly IPage _page;

        public CartPage(IPage page) => _page = page;

        public async Task AddItemToCart(int itemIndex = 1)
        {
            var item = _page.Locator($"(//a[contains(text(),'Add to cart')])[{itemIndex}]");
            await item.ScrollIntoViewIfNeededAsync();
            await item.ClickAsync(new LocatorClickOptions { Force = true });
        }

        public async Task ContinueShoppingFromModal()
        {
            var continueButton = _page.Locator("button[data-dismiss='modal']");
            await continueButton.ClickAsync();
            await _page.WaitForTimeoutAsync(1000);
        }

        public async Task ViewCartFromModal()
        {
            var viewCartButton = _page.Locator("//u[contains(text(),'View Cart')]");
            await viewCartButton.ClickAsync();
        }

        public async Task EnsureCartPageLoaded()
        {
            await _page.WaitForURLAsync("**/view_cart");
            await _page.Locator(".cart_info").WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible,
                Timeout = 10000
            });
        }

        public async Task UpdateQuantity(int rowIndex, string quantity)
        {
            await _page.FillAsync($"tr:nth-child({rowIndex}) input.cart_quantity_input", quantity);
        }

        public async Task RemoveItem(int rowIndex)
        {
            await _page.ClickAsync($"tr:nth-child({rowIndex}) a.cart_quantity_delete");
        }

        public async Task<bool> ValidateCartTotal(int rowIndex)
        {
           var priceText = await _page.Locator($"tr:nth-child({rowIndex}) td.cart_price p").InnerTextAsync();
           var quantityButtonText = await _page.Locator($"tr:nth-child({rowIndex}) td.cart_quantity button").InnerTextAsync();
           var totalText = await _page.Locator($"tr:nth-child({rowIndex}) td.cart_total p").InnerTextAsync();

          int price = int.Parse(priceText.Replace("Rs. ", "").Trim());
          int quantity = int.Parse(quantityButtonText.Trim());
          int total = int.Parse(totalText.Replace("Rs. ", "").Trim());

           return total == price * quantity;
        }
    }
}
