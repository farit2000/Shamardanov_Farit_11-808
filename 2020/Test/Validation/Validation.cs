using System;

namespace Test.Validation
{
    public static class Validation
    {
        public static ValidationResult Validate(object postEntry)
        {
            var properties = postEntry.GetType().GetProperties();
            foreach (var prop in properties)
            {
                var attrs = prop.GetCustomAttributes(false);
                foreach (ValidationAttribute attr in attrs)
                {
                    if (!attr.IsValid(prop.GetValue(postEntry)))
                    {
                        Console.WriteLine("Error");
                        return new ValidationResult(false, $"{prop.Name} isn't valid");
                    }
                }
            }
            
            return new ValidationResult(true);
        }
    }
}