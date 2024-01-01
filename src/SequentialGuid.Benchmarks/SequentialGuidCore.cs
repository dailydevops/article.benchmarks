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

    public static Guid GuidAsBase()
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

        return new Guid(guidBytes);
    }

    public static Guid Stackalloc()
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

        return new Guid(guidBytes);
    }

    public static Guid StackallocWithRandom()
    {
        var timeStamp = DateTime.UtcNow.Ticks / 10000L;
        var timeStampBytes = BitConverter.GetBytes(timeStamp).AsSpan();

        if (BitConverter.IsLittleEndian)
        {
            timeStampBytes.Reverse();
        }

        Span<byte> guidBytes = stackalloc byte[16];
        _random.NextBytes(guidBytes);

        timeStampBytes.Slice(2, 6).CopyTo(guidBytes.Slice(10, 6));

        return new Guid(guidBytes);
    }

    public static Guid CreateAdp()
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

        return new Guid(guidBytes);
    }
}
