namespace ZLinq
{
    partial class ValueEnumerableExtensions
    {
        public static ExceptByValueEnumerable<TEnumerable, TSource, TKey> ExceptBy<TEnumerable, TSource, TKey>(this TEnumerable source, IEnumerable<TKey> second, Func<TSource, TKey> keySelector)
            where TEnumerable : struct, IValueEnumerable<TSource>
#if NET9_0_OR_GREATER
            , allows ref struct
#endif
            => new(source, second, keySelector);

        public static ExceptByValueEnumerable2<TEnumerable, TSource, TKey> ExceptBy<TEnumerable, TSource, TKey>(this TEnumerable source, IEnumerable<TKey> second, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
            where TEnumerable : struct, IValueEnumerable<TSource>
#if NET9_0_OR_GREATER
            , allows ref struct
#endif
            => new(source, second, keySelector, comparer);

    }
}

namespace ZLinq.Linq
{
    [StructLayout(LayoutKind.Auto)]
    [EditorBrowsable(EditorBrowsableState.Never)]
#if NET9_0_OR_GREATER
    public ref
#else
    public
#endif
    struct ExceptByValueEnumerable<TEnumerable, TSource, TKey>(TEnumerable source, IEnumerable<TKey> second, Func<TSource, TKey> keySelector)
        : IValueEnumerable<TSource>
        where TEnumerable : struct, IValueEnumerable<TSource>
#if NET9_0_OR_GREATER
        , allows ref struct
#endif
    {
        TEnumerable source = source;

        public ValueEnumerator<ExceptByValueEnumerable<TEnumerable, TSource, TKey>, TSource> GetEnumerator() => new(this);

        public bool TryGetNonEnumeratedCount(out int count)
        {
            throw new NotImplementedException();
            // return source.TryGetNonEnumeratedCount(count);
            // count = 0;
            // return false;
        }

        public bool TryGetSpan(out ReadOnlySpan<TSource> span)
        {
            throw new NotImplementedException();
            // span = default;
            // return false;
        }

        public bool TryGetNext(out TSource current)
        {
            throw new NotImplementedException();
            // Unsafe.SkipInit(out current);
            // return false;
        }

        public void Dispose()
        {
            source.Dispose();
        }
    }

    [StructLayout(LayoutKind.Auto)]
    [EditorBrowsable(EditorBrowsableState.Never)]
#if NET9_0_OR_GREATER
    public ref
#else
    public
#endif
    struct ExceptByValueEnumerable2<TEnumerable, TSource, TKey>(TEnumerable source, IEnumerable<TKey> second, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
        : IValueEnumerable<TSource>
        where TEnumerable : struct, IValueEnumerable<TSource>
#if NET9_0_OR_GREATER
        , allows ref struct
#endif
    {
        TEnumerable source = source;

        public ValueEnumerator<ExceptByValueEnumerable2<TEnumerable, TSource, TKey>, TSource> GetEnumerator() => new(this);

        public bool TryGetNonEnumeratedCount(out int count)
        {
            throw new NotImplementedException();
            // return source.TryGetNonEnumeratedCount(count);
            // count = 0;
            // return false;
        }

        public bool TryGetSpan(out ReadOnlySpan<TSource> span)
        {
            throw new NotImplementedException();
            // span = default;
            // return false;
        }

        public bool TryGetNext(out TSource current)
        {
            throw new NotImplementedException();
            // Unsafe.SkipInit(out current);
            // return false;
        }

        public void Dispose()
        {
            source.Dispose();
        }
    }

}
