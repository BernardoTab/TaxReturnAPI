namespace Tax.Entities.Exceptions
{
    public class InvalidInputTypeException : Exception, IKnownException
    {
        public object Value { get; set; }
        public string PropertyName { get; set; }

        public InvalidInputTypeException(object value, string propertyName)
            : base($"The type of value {value} of property {propertyName} is not valid. A non-zero positive decimal value is expected.")
        {
            Value = value;
            PropertyName = propertyName;
        }
    }
}
