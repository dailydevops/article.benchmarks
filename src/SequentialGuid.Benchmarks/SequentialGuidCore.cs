namespace SequentialGuid.Benchmarks;

using System;
using System.Security.Cryptography;

public static class SequentialGuidCore
{
    private static readonly RandomNumberGenerator _random = RandomNumberGenerator.Create();

    public static string GuidAsBase()
    {
        var timeStamp = DateTime.UtcNow.Ticks / 10000L;
        Span<byte> timeStampBytes = BitConverter.GetBytes(timeStamp);

        if (BitConverter.IsLittleEndian)
        {
            timeStampBytes.Reverse();
        }

        Span<byte> guidBytes = Guid.NewGuid().ToByteArray();
        _random.GetBytes(guidBytes);

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
        _random.GetBytes(guidBytes);

        timeStampBytes.Slice(2, 6).CopyTo(guidBytes.Slice(10, 6));

        return new Guid(guidBytes).ToString("N");
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
        _random.GetBytes(guidBytes);

        timeStampBytes.Slice(2, 6).CopyTo(guidBytes.Slice(10, 6));

#pragma warning disable CA1308 // Normalize strings to uppercase
        return Convert.ToHexString(guidBytes);
#pragma warning restore CA1308 // Normalize strings to uppercase
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
        _random.GetBytes(guidBytes);

        timeStampBytes.Slice(2, 6).CopyTo(guidBytes.Slice(10, 6));

#pragma warning disable CA1308 // Normalize strings to uppercase
        return BitConverter.ToString(guidBytes.ToArray());
#pragma warning restore CA1308 // Normalize strings to uppercase
    }

    public static string CreateAdp()
    {
        var randomBytes = new byte[10];
        _random.GetBytes(randomBytes);
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
