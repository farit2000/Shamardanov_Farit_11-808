namespace Test.Validation
{
    public class NotEmptyAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null && value.ToString() != "")
                return true;
            ErrorMessage = "Field shouldn't be empty!";
            return false;
        }
    }
}