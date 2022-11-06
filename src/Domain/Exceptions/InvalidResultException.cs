namespace TransfusionAPI.Domain.Exceptions;

public class InvalidResultException : Exception
{
    private InvalidResultException(string message) : base(message)
    {
    }

    public static InvalidResultException PayloadCannotBeNullableWhenSuccessfulResult()
    {
        return new InvalidResultException("Payload cannot be nullable");
    }
}
