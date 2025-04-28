using System;
using System.Collections.Generic;

namespace AutomationExerciseTests.Utilities
{
    public static class UserGenerator
    {
        public static Dictionary<string, string> GenerateRandomUser()
        {
            var random = new Random();
            int randomNum = random.Next(1000, 9999);
            
            return new Dictionary<string, string>
            {
                { "name", $"TestUser{randomNum}" },
                { "email", $"testuser{randomNum}@example.com" },
                { "password", "Password123" },
                { "title", "Mr" },
                { "birth_date", "1" },
                { "birth_month", "January" },
                { "birth_year", "1990" },
                { "firstname", "Test" },
                { "lastname", $"User{randomNum}" },
                { "company", "HexaSystems" },
                { "address1", "123 Main Street" },
                { "address2", "Apt 4B" },
                { "country", "United States" },
                { "zipcode", "12345" },
                { "state", "California" },
                { "city", "Los Angeles" },
                { "mobile_number", "1234567890" }
            };
        }
    }
}
