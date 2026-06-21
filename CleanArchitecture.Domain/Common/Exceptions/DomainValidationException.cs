namespace CleanArchitecture.Domain.Common.Exceptions
{
    public class DomainValidationException(string message) : Exception(message);
}
