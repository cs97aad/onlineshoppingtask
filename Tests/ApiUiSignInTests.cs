using System;
using NUnit.Framework;
using System.Threading.Tasks;
using AutomationExerciseTests.Utilities;
using System.Collections.Generic;
using Microsoft.Playwright;



namespace AutomationExerciseTests.Tests
{
    public class ApiUiSignInTests : TestBase
    {
        private Dictionary<string, string> _userData;

        [Test]
        public async Task UserCanSignInUsingApiCreatedAccount()
        {
            // Step 1: Create User via API
            var apiClient = new ApiClient();
            _userData = UserGenerator.GenerateRandomUser();
            var response = await apiClient.CreateAccountAsync(_userData);

            Assert.AreEqual(200, (int)response.StatusCode, "API should return 200 Created"); // API spec says 201 should be returned, but 200 is being returned.

            // Step 2: Navigate to Homepage
            await _page.GotoAsync("https://automationexercise.com/");

            // Step 3: Click 'Signup / Login'
            await _page.ClickAsync("a[href='/login']");

            // Step 4: Verify 'Login to your account' is visible
            Assert.IsTrue(await _page.Locator("h2:has-text('Login to your account')").IsVisibleAsync());

            // Step 5: Fill email and password
            await _page.FillAsync("input[data-qa='login-email']", _userData["email"]);
            await _page.FillAsync("input[data-qa='login-password']", _userData["password"]);

            // Print generated user details
             Console.WriteLine($"Generated Test Email: {_userData["email"]}");
             Console.WriteLine($"Generated Test Password: {_userData["password"]}");

            // Step 6: Click Login button
            await _page.ClickAsync("button[data-qa='login-button']");

            //  Wait until "Logged in as" element appears (up to 10 seconds)
            var loggedInUserElement = _page.Locator("a:has(i.fa-user)");
            await loggedInUserElement.WaitForAsync(new LocatorWaitForOptions { Timeout = 10000 });

            // Step 7: Verify Logged in as firstname.lastname
            // Verify logged in
             var loggedInText = await loggedInUserElement.InnerTextAsync();
             Assert.IsTrue(loggedInText.Contains(_userData["name"]), $"Expected logged-in username '{_userData["name"]}' was not found.");
        }
    }
}
