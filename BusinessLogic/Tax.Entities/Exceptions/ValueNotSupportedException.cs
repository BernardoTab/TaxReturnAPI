namespace Tax.Entities.Exceptions
{
    public class ValueNotSupportedException : Exception
    {
        public object Value { get; set; }
        public string PropertyName { get; set; }
        public string OwnerName { get; set; }

        public ValueNotSupportedException(object value, string propertyName, string ownerName)
            : base($"The value {value} of property {ownerName}.{propertyName} is not valid.")
        {
            Value = value;
            PropertyName = propertyName;
            OwnerName = ownerName;
        }
    }
}
