namespace Domain.Errors;

public static class InvalidPersonErrors
{
    public static KeyValuePair<string, string> InvalidNameError =
        new KeyValuePair<string, string>("Name Invalid", "Name must be no more than 50 characters long.");

    public static KeyValuePair<string, string> InvalidAgeError =
        new KeyValuePair<string, string>("Age Invalid", "Age has to be greater than 0.");
}
