namespace Test.Validation
{
    public abstract class ValidationAttribute : System.Attribute
    {
        public string ErrorMessage { get; set; }
        public abstract bool IsValid(object value);
    }
}