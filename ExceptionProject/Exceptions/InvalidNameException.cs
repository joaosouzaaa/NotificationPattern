using Domain.Errors;
using System.Net;

namespace ExceptionProject.Exceptions;

public sealed class InvalidNameException : Exception
{
    private const HttpStatusCode _invalidNameHttpStatusCode = HttpStatusCode.BadRequest;
    public HttpStatusCode StatusCode 
    {
        get => _invalidNameHttpStatusCode;
        private set => value = _invalidNameHttpStatusCode;
    }

    private string _key = InvalidPersonErrors.InvalidNameError.Key;
    public string Key 
    {
        get => _key;
        private set => value = _key;
    }

    public InvalidNameException() : base(InvalidPersonErrors.InvalidNameError.Value)
    {

    }
}
