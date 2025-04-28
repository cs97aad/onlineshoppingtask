# AutomationExerciseTests - Playwright C#/.NET Hybrid Automation Framework

---

##  Project Overview

This project automates **AutomationExercise.com** workflows, combining **UI Testing** and **API Testing** into a hybrid, scalable framework.  
Built using **Microsoft Playwright** with **C# and .NET 6**.

---

## Setup Instructions

1. **Clone the Repository**

```bash
git clone <your-repo-url>
```

2. **Install Required Tools**

- Install .NET 6 SDK
- Install Visual Studio Code or Visual Studio
- Install Playwright CLI:

```bash
dotnet tool install --global Microsoft.Playwright.CLI
```

3. **Restore Dependencies**

```bash
dotnet restore
```

4. **Install Browsers**

```bash
playwright install
```

---

## Test Execution

- **Build the project:**

```bash
dotnet build
```

- **Run Tests:**

```bash
dotnet test --logger "trx;LogFileName=TestResults.trx"
```

- **Generate HTML Reports:**

```bash
reportgenerator -reports:TestResults.trx -targetdir:Reports -reporttypes:Html
```

- **Open Report:**

```bash
open Reports/index.html
```

- **Quick Run Script:**

```bash
bash runtests.sh
```

---

##  Test Architecture and Patterns

| Component                 | Purpose                                           |
|:---------------------------|:--------------------------------------------------|
| `Utilities/TestBase.cs`    | Browser setup, teardown, cookie handling          |
| `Utilities/ApiClient.cs`   | API interactions (e.g., Create Account)            |
| `Utilities/UserGenerator.cs` | Randomized test data generation                 |
| `Pages/`                   | Page Object Model (POM) for modular UI automation |
| `Tests/`                   | Test cases combining API + UI verification       |

### Key Patterns:
- **Page Object Model (POM)** for modularity.
- **Hybrid Testing** (API for setup + UI for validation).
- **Auto Screenshots on Failure** for easier debugging.
- **HTML Report Generation** after each test run.

---

##  Reasoning Behind Key Decisions

| Decision                    | Reason                                           |
|:----------------------------|:-------------------------------------------------|
| API for test setup          | Speeds up preconditions (no UI overhead)         |
| Random user generation      | Eliminates duplicate data issues                 |
| Smart waits vs fixed sleeps | Reduces test flakiness                           |
| One-click script            | Simplifies local runs and CI/CD setup            |

---

##  Challenges and Solutions

| Challenge                        | Solution                                           |
|:----------------------------------|:--------------------------------------------------|
| Cookie popup blocking UI flows   | Handled automatically via `HandleCookieConsentAsync()` |
| Username mismatch (API vs UI)    | Parsed `firstname.lastname` for validation |
| Flaky timing issues              | Implemented `WaitForAsync()` smart waiting strategy |
| Maintaining user data integrity  | Dynamically generated emails & passwords, logged to console |

---

##  Potential Improvements

- Implement full retry logic for flaky UI steps
- Parallelize tests for faster execution (Playwright workers)
- BrowserStack/SauceLabs integration for cross-browser/cloud testing
- Dockerized CI environment
- Integrate advanced reporting (Allure or ExtentReports)
- Sophisticated event-driven logging (e.g., attach screenshots per step)
- Create a custom extension for the Playwright test framework that adds functionality not available out-of-the-box (e.g., custom retry logic, specific reporting feature, or specialized assertions)
- Implement a less common design pattern beyond POM (e.g., Screenplay Pattern, Fluent Interface Pattern) and explain why you chose it
- Create tests specifically designed to handle dynamic elements, network latency, or other real-world challenges, with detailed comments explaining your approach

---

##  Inline Code Commenting Example

```csharp
// Wait until the 'Logged in as' user element appears
await loggedInUserElement.WaitForAsync(new() { Timeout = 10000 });

// Verify if the generated username appears correctly
var loggedInText = await loggedInUserElement.InnerTextAsync();
Assert.IsTrue(loggedInText.Contains(_userData["name"]));
```

---

## License

This project is licensed under the MIT License.

---

##  Originality Statement

This project and all its contents, including architecture, code implementations, and documentation, are my original work. No external codebases were directly copied. I referred to official documentation and best practices from:
- [Microsoft Playwright for .NET Documentation](https://playwright.dev/dotnet/)
- [NUnit Testing Framework Documentation](https://nunit.org/)
- [AutomationExercise.com API Documentation](https://automationexercise.com/api_list)
- Learning C# (https://www.w3schools.com/cs/index.php)

All third-party dependencies are properly attributed via NuGet packages and official documentation references.


