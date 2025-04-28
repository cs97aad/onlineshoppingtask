using Microsoft.Playwright;
using System.Linq;
using System.Threading.Tasks;

namespace AutomationExerciseTests.Pages
{
    public class SearchPage
    {
        private readonly IPage _page;

        public SearchPage(IPage page) => _page = page;

        // Step 4: Navigate to Products page
        public async Task NavigateToProductsPage()
        {
            await _page.ClickAsync("a[href='/products']");
            await _page.WaitForURLAsync("**/products");
        }

        // Step 5: Verify user is on Products page
        public async Task<bool> IsProductsPageVisible()
        {
            return await _page.Locator(".features_items").IsVisibleAsync();
        }

        // Step 6: Enter product name and search
        public async Task SearchForProduct(string query)
        {
            await _page.FillAsync("input[name='search']", query);
            await _page.ClickAsync("button[id='submit_search']");
        }

        // Step 7: Verify 'SEARCHED PRODUCTS' is visible
        public async Task<bool> IsSearchResultsVisible()
        {
            return await _page.Locator("h2.title.text-center")
                .Filter(new LocatorFilterOptions { HasTextString = "Searched Products" })
                .IsVisibleAsync();
        }

        // Step 8: Apply a filter (example: 'Polo')
        public async Task ApplyCategoryFilter(string filterName)
        {
            await _page.ClickAsync($"a[href='/brand_products/{filterName}']");
        }

        //  Step 9: Verify filtered search results
        public async Task<bool> IsFilterAppliedCorrectly(string filterName)
        {
            var productNames = await _page.Locator(".productinfo.text-center p").AllInnerTextsAsync();
            return productNames.Any(name => name.ToLower().Contains(filterName.ToLower()));
        }
    }
}
