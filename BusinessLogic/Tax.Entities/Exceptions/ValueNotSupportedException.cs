namespace Tax.Entities.Exceptions
{
    public class ValueNotSupportedException : Exception, IKnownException
    {
        public object Value { get; set; }
        public string PropertyName { get; set; }

        public ValueNotSupportedException(object value, string propertyName)
            : base($"The value {value} of property {propertyName} is not valid. You must set it to a non-zero positive number.")
        {
            Value = value;
            PropertyName = propertyName;
        }
    }
}
