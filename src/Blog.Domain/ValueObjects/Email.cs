using Blog.Domain.ValueObjects.Base;
using System.Text.RegularExpressions;

namespace Blog.Domain.ValueObjects;

public partial class Email : ValueObject
{
    private const string Pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

    public Email(string address)
    {
        if (string.IsNullOrEmpty(address))
            throw new Exception("Invalid email address");

        Address = address.Trim().ToLower();

        if (Address.Length < 5)
            throw new Exception("Invalid email address");

        if (!EmailRegex().IsMatch(Address))
            throw new Exception("Invalid email address");
    }

    public string Address { get; }

    public static implicit operator string(Email email)
        => email.ToString();

    public static implicit operator Email(string address)
        => new(address);

    public override string ToString()
        => Address;

    [GeneratedRegex(Pattern)]
    private static partial Regex EmailRegex();

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Address;
    }
}