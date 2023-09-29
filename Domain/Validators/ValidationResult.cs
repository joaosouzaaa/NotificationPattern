namespace Domain.Validators;
public sealed class ValidationResult
{
    public required bool IsValid { get; set; }
    public Dictionary<string, string> Errors { get; set; } = new Dictionary<string, string>();
}
