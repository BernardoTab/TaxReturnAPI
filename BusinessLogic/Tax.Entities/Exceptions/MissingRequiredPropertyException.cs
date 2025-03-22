namespace Tax.Entities.Exceptions
{
    public class MissingRequiredPropertyException : Exception, IKnownException
    {
        public string PropertyName { get; set; }

        public MissingRequiredPropertyException(string propertyName)
            : base($"The required property {propertyName} is missing or invalid.")
        {
            PropertyName = propertyName;
        }
    }
}
