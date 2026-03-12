using System;

namespace StudiePlusPlus.Domain.ValueObjects;

public sealed record Email
{
    public string Value { get; }

    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Email cannot be empty.", nameof(value));

        if (!value.Contains('@'))
            throw new ArgumentException("Email must contain '@'.", nameof(value));

        Value = value.Trim();
    }

    public override string ToString() => Value;
}