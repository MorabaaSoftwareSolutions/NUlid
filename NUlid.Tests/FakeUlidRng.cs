﻿using System;
using System.Linq;
using NUlid.Rng;

namespace NUlid.Tests;

/// <summary>
/// A fake RNG for the random part of ulid's.
/// </summary>
/// <remarks>
/// This can be used for unittests since it the returned 'random bytes' are known beforehand.
/// </remarks>
public class FakeUlidRng : IUlidRng
{
    //Values specifically chosen to make the result spell DEADBEEFDEADBEEF
    public static readonly byte[] DEFAULTRESULT = new byte[] { 107, 148, 213, 185, 207, 107, 148, 213, 185, 207 };
    private readonly byte[] _desiredresult;

    public FakeUlidRng()
        : this(DEFAULTRESULT.ToArray()) { }     // make a copy

    public FakeUlidRng(byte[] desiredResult) => _desiredresult = desiredResult;

    public byte[] GetRandomBytes(DateTimeOffset dateTime) => _desiredresult;

    public void GetRandomBytes(Span<byte> buffer, DateTimeOffset dateTime) => _desiredresult.AsSpan().CopyTo(buffer);
}
