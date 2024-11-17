using System.Text.RegularExpressions;
using Bogus;

namespace Spec.Helpers;

public static class BogusGenerator
{
    private static Faker? _faker;

    public static string Generate(string payload)
    {
        _faker ??= new Faker();

        return Regex.Replace(payload, @"<(\w+)(?::(\d+))?>", match =>
        {
            string placeholderMatch = match.Groups[1].Value;
            string lengthMatch = match.Groups[2].Value;
            int length = !string.IsNullOrEmpty(lengthMatch) ? int.Parse(lengthMatch) : 0;

            return placeholderMatch switch
            {
                "long_text" => _faker.Random.AlphaNumeric(length > 0 ? length : 100),
                _ => match.Value
            };
        });
    }
}