﻿namespace ZLinq;

// This struct is wrapper for enumerator(enumerable) to improve type inference in C# compiler.
// C# constraint inference issue: https://github.com/dotnet/csharplang/discussions/6930
[StructLayout(LayoutKind.Auto)]
#if NET9_0_OR_GREATER
public readonly ref
#else
public readonly
#endif
struct ValueEnumerable<TEnumerator, T>(TEnumerator enumerator)
    where TEnumerator : struct, IValueEnumerator<T>
#if NET9_0_OR_GREATER
    , allows ref struct
#endif
{
    // enumerator is struct so it always copied, no need to create new Enumerator.
    // internal operator should use this
    public readonly TEnumerator Enumerator = enumerator;
}

// all implement types must be struct
public interface IValueEnumerator<T> : IDisposable
{
    bool TryGetNext(out T current); // as MoveNext + Current

    // Optimization helper
    bool TryGetNonEnumeratedCount(out int count);
    bool TryGetSpan(out ReadOnlySpan<T> span);
    bool TryCopyTo(Span<T> destination);
}

// generic implementation of enumerator
[StructLayout(LayoutKind.Auto)]
#if NET9_0_OR_GREATER
public ref
#else
public
#endif
struct ValueEnumerator<TEnumerator, T>(TEnumerator enumerator) : IDisposable
    where TEnumerator : struct, IValueEnumerator<T>
#if NET9_0_OR_GREATER
    , allows ref struct
#endif
{
    TEnumerator enumerator = enumerator;
    T current = default!;

    public T Current
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => current;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool MoveNext() => enumerator.TryGetNext(out current);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Dispose() => enumerator.Dispose();
}

public static partial class ValueEnumerableExtensions // keep `public static` partial class
{
    // for foreach
    public static ValueEnumerator<TEnumerator, T> GetEnumerator<TEnumerator, T>(in this ValueEnumerable<TEnumerator, T> valueEnumerable)
        where TEnumerator : struct, IValueEnumerator<T>
#if NET9_0_OR_GREATER
        , allows ref struct
#endif
        => new(valueEnumerable.Enumerator);
}
