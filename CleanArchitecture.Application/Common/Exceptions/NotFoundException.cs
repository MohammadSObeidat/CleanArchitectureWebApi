namespace CleanArchitecture.Application.Common.Exceptions
{
    public class NotFoundException(string entity, int id) : Exception($"{entity} with ID '{id}' was not found");
}
