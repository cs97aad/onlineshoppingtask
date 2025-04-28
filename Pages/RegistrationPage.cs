using Microsoft.Playwright;
using System.Threading.Tasks;

namespace AutomationExerciseTests.Pages
{
    public class RegistrationPage
    {
        private readonly IPage _page;

        public RegistrationPage(IPage page) => _page = page;

        public async Task FillSignupForm(string name, string email)
        {
            await _page.FillAsync("input[data-qa='signup-name']", name);
            await _page.FillAsync("input[data-qa='signup-email']", email);
            await _page.ClickAsync("button[data-qa='signup-button']");
        }

        public async Task CompleteAccountDetails(string password)
        {
            await _page.CheckAsync("#id_gender1");
            await _page.FillAsync("#password", password);
            await _page.SelectOptionAsync("#days", "1");
            await _page.SelectOptionAsync("#months", "1");
            await _page.SelectOptionAsync("#years", "2000");
            await _page.ClickAsync("#newsletter");
            await _page.FillAsync("#first_name", "John");
            await _page.FillAsync("#last_name", "Doe");
            await _page.FillAsync("#address1", "123 Main St");
            await _page.SelectOptionAsync("#country", "Canada");
            await _page.FillAsync("#state", "Ontario");
            await _page.FillAsync("#city", "Toronto");
            await _page.FillAsync("#zipcode", "M1A1A1");
            await _page.FillAsync("#mobile_number", "1234567890");
            await _page.ClickAsync("button[data-qa='create-account']");
        }

        public async Task<bool> IsAccountCreated() =>
            await _page.IsVisibleAsync("h2[data-qa='account-created']");
    }
}
