using Domain.Errors;
using System.Net;

namespace ExceptionProject.Exceptions;

public class InvalidAgeException : Exception
{
    private const HttpStatusCode _invalidNameHttpStatusCode = HttpStatusCode.BadRequest;
    public HttpStatusCode StatusCode
    {
        get => _invalidNameHttpStatusCode;
        private set => value = _invalidNameHttpStatusCode;
    }

    private string _key = InvalidPersonErrors.InvalidAgeError.Key;
    public string Key
    {
        get => _key;
        private set => value = _key;
    }

    public InvalidAgeException() : base(InvalidPersonErrors.InvalidAgeError.Value)
    {

    }
}
