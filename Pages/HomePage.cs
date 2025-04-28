using Microsoft.Playwright;
using System.Threading.Tasks;

namespace AutomationExerciseTests.Pages
{
    public class HomePage
    {
        private readonly IPage _page;

        public HomePage(IPage page) => _page = page;

        public async Task NavigateToHomePage() =>
            await _page.GotoAsync("https://automationexercise.com");

        public async Task ClickSignupLogin() =>
            await _page.ClickAsync("a[href='/login']");
    }
}
