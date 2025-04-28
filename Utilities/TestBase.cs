using Microsoft.Playwright;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AutomationExerciseTests.Utilities
{
    public class TestBase
    {
        protected IBrowser _browser;
        protected IPage _page;
        protected IPlaywright _playwright;
        private string _screenshotsDir = Path.Combine(Directory.GetCurrentDirectory(), "Screenshots");

        [SetUp]
        public async Task Setup()
        {
            _playwright = await Playwright.CreateAsync();
            _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false
            });

            var context = await _browser.NewContextAsync();
            _page = await context.NewPageAsync();

            await _page.GotoAsync("https://automationexercise.com/");
            await HandleCookieConsentAsync();
        }

        [TearDown]
        public async Task Teardown()
        {
            var outcome = TestContext.CurrentContext.Result.Outcome.Status;
            if (outcome == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                await TakeScreenshot(TestContext.CurrentContext.Test.Name);
            }

            await _browser.CloseAsync();
            _playwright.Dispose();
        }

        private async Task TakeScreenshot(string testName)
        {
            if (!Directory.Exists(_screenshotsDir))
            {
                Directory.CreateDirectory(_screenshotsDir);
            }

            var screenshotPath = Path.Combine(_screenshotsDir, $"{testName}_{DateTime.Now:yyyyMMdd_HHmmss}.png");
            await _page.ScreenshotAsync(new PageScreenshotOptions { Path = screenshotPath });
            TestContext.AddTestAttachment(screenshotPath, "Screenshot on Failure");
        }

        private async Task HandleCookieConsentAsync()
        {
            var consentButton = _page.Locator("button:has-text('Consent')");
            if (await consentButton.IsVisibleAsync())
            {
                await consentButton.ClickAsync();
            }
        }

    }
}