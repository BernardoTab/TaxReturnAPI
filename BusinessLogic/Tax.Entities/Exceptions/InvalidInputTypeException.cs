namespace Tax.Entities.Exceptions
{
    public class InvalidInputTypeException : Exception, IKnownException
    {
        public string PropertyName { get; set; }

        public InvalidInputTypeException(string propertyName)
            : base($"The type of input property {propertyName} is not valid. A non-zero positive decimal value is expected.")
        {
            PropertyName = propertyName;
        }
    }
}
