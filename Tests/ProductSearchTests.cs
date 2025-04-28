using NUnit.Framework;
using System.Threading.Tasks;
using AutomationExerciseTests.Pages;
using AutomationExerciseTests.Utilities;

namespace AutomationExerciseTests.Tests
{
    public class ProductSearchTests : TestBase
    {
        [Test]
        public async Task UserCanSearchAndFilterProducts()
        {
            var searchPage = new SearchPage(_page);

            // Step 4: Click 'Products' button first
            await searchPage.NavigateToProductsPage();

            // Step 5: Verify user is navigated to ALL PRODUCTS page
            bool isProductsPageVisible = await searchPage.IsProductsPageVisible();
            Assert.IsTrue(isProductsPageVisible, "User should be on All Products page.");

            //  Step 6: Search for product
            await searchPage.SearchForProduct("tshirt");

            // Step 7: Verify 'SEARCHED PRODUCTS' is visible
            bool isSearchResultsVisible = await searchPage.IsSearchResultsVisible();
            Assert.IsTrue(isSearchResultsVisible, "Searched products should be visible.");

            // Step 8: Apply a filter (e.g., Polo)
            await searchPage.ApplyCategoryFilter("Polo");

            // Step 9: Verify search results match filter
            bool isFilterCorrect = await searchPage.IsFilterAppliedCorrectly("Polo");
            Assert.IsTrue(isFilterCorrect, "Filtered products should match 'Polo'.");
        }
    }
}
