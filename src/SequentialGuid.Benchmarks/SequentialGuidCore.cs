namespace SequentialGuid.Benchmarks;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;

[SuppressMessage(
    "Security",
    "CA5394:Do not use insecure randomness",
    Justification = "As designed."
)]
public static class SequentialGuidCore
{
    private static readonly RandomNumberGenerator _randomNumber = RandomNumberGenerator.Create();
    private static readonly Random _random = Random.Shared;

    public static string GuidAsBase()
    {
        var timeStamp = DateTime.UtcNow.Ticks / 10000L;
        Span<byte> timeStampBytes = BitConverter.GetBytes(timeStamp);

        if (BitConverter.IsLittleEndian)
        {
            timeStampBytes.Reverse();
        }

        Span<byte> guidBytes = Guid.NewGuid().ToByteArray();
        _randomNumber.GetBytes(guidBytes);

        timeStampBytes.Slice(2, 6).CopyTo(guidBytes.Slice(10, 6));

        return new Guid(guidBytes).ToString("N");
    }

    public static string Stackalloc()
    {
        var timeStamp = DateTime.UtcNow.Ticks / 10000L;
        Span<byte> timeStampBytes = BitConverter.GetBytes(timeStamp);

        if (BitConverter.IsLittleEndian)
        {
            timeStampBytes.Reverse();
        }

        Span<byte> guidBytes = stackalloc byte[16];
        _randomNumber.GetBytes(guidBytes);

        timeStampBytes.Slice(2, 6).CopyTo(guidBytes.Slice(10, 6));

        return new Guid(guidBytes).ToString("N");
    }

    public static string StackallocWithRandom()
    {
        var timeStamp = DateTime.UtcNow.Ticks / 10000L;
        Span<byte> timeStampBytes = BitConverter.GetBytes(timeStamp);

        if (BitConverter.IsLittleEndian)
        {
            timeStampBytes.Reverse();
        }

        Span<byte> guidBytes = stackalloc byte[16];
        _random.NextBytes(guidBytes);

        timeStampBytes.Slice(2, 6).CopyTo(guidBytes.Slice(10, 6));

        return new Guid(guidBytes).ToString("N");
    }

    public static string StackallocGuidParse()
    {
        var timeStamp = DateTime.UtcNow.Ticks / 10000L;
        Span<byte> timeStampBytes = BitConverter.GetBytes(timeStamp);

        if (BitConverter.IsLittleEndian)
        {
            timeStampBytes.Reverse();
        }

        ReadOnlySpan<byte> guidBytes = stackalloc byte[16];
        _random.NextBytes(guidBytes);

        timeStampBytes.Slice(2, 6).CopyTo(guidBytes.Slice(10, 6));

        return Guid.Parse(guidBytes).ToString("N");
    }

    public static string StackallocHex()
    {
        var timeStamp = DateTime.UtcNow.Ticks / 10000L;
        Span<byte> timeStampBytes = BitConverter.GetBytes(timeStamp);

        if (BitConverter.IsLittleEndian)
        {
            timeStampBytes.Reverse();
        }

        Span<byte> guidBytes = stackalloc byte[16];
        _randomNumber.GetBytes(guidBytes);

        timeStampBytes.Slice(2, 6).CopyTo(guidBytes.Slice(10, 6));

        return Convert.ToHexString(guidBytes);
    }

    public static string StackallocToString()
    {
        var timeStamp = DateTime.UtcNow.Ticks / 10000L;
        Span<byte> timeStampBytes = BitConverter.GetBytes(timeStamp);

        if (BitConverter.IsLittleEndian)
        {
            timeStampBytes.Reverse();
        }

        Span<byte> guidBytes = stackalloc byte[16];
        _randomNumber.GetBytes(guidBytes);

        timeStampBytes.Slice(2, 6).CopyTo(guidBytes.Slice(10, 6));

        return BitConverter.ToString(guidBytes.ToArray());
    }

    public static string CreateAdp()
    {
        var randomBytes = new byte[10];
        _randomNumber.GetBytes(randomBytes);
        var timestamp = DateTime.UtcNow.Ticks / 10000L;
        var timestampBytes = BitConverter.GetBytes(timestamp);
        if (BitConverter.IsLittleEndian)
        {
            Array.Reverse(timestampBytes);
        }

        var guidBytes = new byte[16];

        // For sequential-at-the-end versions, we copy the random data first,
        // followed by the timestamp.
        Buffer.BlockCopy(randomBytes, 0, guidBytes, 0, 10);
        Buffer.BlockCopy(timestampBytes, 2, guidBytes, 10, 6);

        return new Guid(guidBytes).ToString("N");
    }
}
