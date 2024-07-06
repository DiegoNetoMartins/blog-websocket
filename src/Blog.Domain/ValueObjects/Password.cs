using Blog.Domain.ValueObjects.Base;
using System.Security.Cryptography;

namespace Blog.Domain.ValueObjects;

public class Password : ValueObject
{
    private const string _valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%&*";
    private const short _saltSize = 16;
    private const short _KeySize = 32;
    private const int _iterations = 10000;
    private const char _splitChar = '.';
    private readonly Guid _userId;

#pragma warning disable CS8618 // O campo não anulável precisa conter um valor não nulo ao sair do construtor. Considere declará-lo como anulável.
    protected Password() { }
#pragma warning restore CS8618 // O campo não anulável precisa conter um valor não nulo ao sair do construtor. Considere declará-lo como anulável.

    public Password(Guid userId, string? text = null)
    {
        _userId = userId;
        if (string.IsNullOrWhiteSpace(text))
        {
            text = Generate();
            Hash = Hashing(text);
        }
        else
        {
            Hash = Hashing(text);
        }
    }

    public string Hash { get; private set; }
    
    public bool Verify(Guid userId, string plainTextPassword)
        => Verify(userId, Hash, plainTextPassword);

    private static string Generate(short length = 16)
    {
        var index = 0;
        var res = new char[length];
        var rnd = new Random();

        while (index < length)
            res[index++] = _valid[rnd.Next(0, _valid.Length)];

        return new string(res);
    }

    private string Hashing(string password)
    {
        if (string.IsNullOrEmpty(password))
            throw new Exception("Password should not be null or empty");

        password += _userId;

        using var algorithm = new Rfc2898DeriveBytes(
            password,
            _saltSize,
            _iterations,
            HashAlgorithmName.SHA256);
        var key = Convert.ToBase64String(algorithm.GetBytes(_KeySize));
        var salt = Convert.ToBase64String(algorithm.Salt);

        return $"{salt}{_splitChar}{key}";
    }

    private bool Verify(Guid userId, string hash, string password)
    {
        password += userId;

        var parts = hash.Split(_splitChar, 2);
        if (parts.Length != 2)
            return false;

        var salt = Convert.FromBase64String(parts[0]);
        var key = Convert.FromBase64String(parts[1]);

        using var algorithm = new Rfc2898DeriveBytes(
            password,
            salt,
            _iterations,
            HashAlgorithmName.SHA256);
        var keyToCheck = algorithm.GetBytes(_KeySize);

        return keyToCheck.SequenceEqual(key);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Hash;
    }
}

