using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Diagnostics;

namespace Test.Validation
{
    public class FirstSymbolCapital : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null && Char.IsUpper(value.ToString().First()))
                return true;
            ErrorMessage = "First symbol is not small";
            return false;
        }
    }
}