using NUnit.Framework;
using AutomationExerciseTests.Utilities;
using AutomationExerciseTests.Pages;
using System.Threading.Tasks;
using System;

namespace AutomationExerciseTests.Tests
{
    public class UserRegistrationTests : TestBase
    {
        [Test]
        public async Task UserCanRegisterSuccessfully()
        {
            var homePage = new HomePage(_page);
            await homePage.NavigateToHomePage();
            await homePage.ClickSignupLogin();

            var registrationPage = new RegistrationPage(_page);
            string uniqueEmail = $"user{Guid.NewGuid().ToString().Substring(0, 6)}@example.com";
            await registrationPage.FillSignupForm("Test User", uniqueEmail);
            await registrationPage.CompleteAccountDetails("P@ssw0rd!");

            Assert.IsTrue(await registrationPage.IsAccountCreated());
        }
    }
}
