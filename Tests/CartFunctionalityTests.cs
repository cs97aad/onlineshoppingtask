using NUnit.Framework;
using System.Threading.Tasks;
using AutomationExerciseTests.Pages;
using AutomationExerciseTests.Utilities; // Make sure this is included!

namespace AutomationExerciseTests.Tests
{
    public class CartFunctionalityTests : TestBase
    {
        [Test]
        public async Task UserCanManageShoppingCart()
        {
            var cartPage = new CartPage(_page);

            // Add first item
            await cartPage.AddItemToCart(1);

            // Continue shopping
            await cartPage.ContinueShoppingFromModal();

            // Add second item
            await cartPage.AddItemToCart(3);

            // View cart
            await cartPage.ViewCartFromModal();

            // Ensure cart page loaded
            await cartPage.EnsureCartPageLoaded();

            //  Validate total for first item
            bool isCartTotalCorrect = await cartPage.ValidateCartTotal(1);
            Assert.IsTrue(isCartTotalCorrect, "First item total price should match price * quantity");

            // Validate total for second item
            isCartTotalCorrect = await cartPage.ValidateCartTotal(2);
            Assert.IsTrue(isCartTotalCorrect, "Second item total price should match price * quantity");

            // Remove second item
            await cartPage.RemoveItem(2);

            // Validate total for remaining first item again
            isCartTotalCorrect = await cartPage.ValidateCartTotal(1);
            Assert.IsTrue(isCartTotalCorrect, "After removing second item, remaining item total should match.");
        }
    }
}
