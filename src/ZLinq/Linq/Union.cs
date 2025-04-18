﻿namespace ZLinq
{
    partial class ValueEnumerableExtensions
    {
        public static ValueEnumerable<Union<TEnumerator, TEnumerator2, TSource>, TSource> Union<TEnumerator, TEnumerator2, TSource>(in this ValueEnumerable<TEnumerator, TSource> source, in ValueEnumerable<TEnumerator2, TSource> second)
            where TEnumerator : struct, IValueEnumerator<TSource>
#if NET9_0_OR_GREATER
            , allows ref struct
#endif
            where TEnumerator2 : struct, IValueEnumerator<TSource>
#if NET9_0_OR_GREATER
            , allows ref struct
#endif
            => new(new(source.Enumerator, second.Enumerator, null));

        public static ValueEnumerable<Union<TEnumerator, TEnumerator2, TSource>, TSource> Union<TEnumerator, TEnumerator2, TSource>(in this ValueEnumerable<TEnumerator, TSource> source, in ValueEnumerable<TEnumerator2, TSource> second, IEqualityComparer<TSource>? comparer)
            where TEnumerator : struct, IValueEnumerator<TSource>
#if NET9_0_OR_GREATER
            , allows ref struct
#endif
            where TEnumerator2 : struct, IValueEnumerator<TSource>
#if NET9_0_OR_GREATER
            , allows ref struct
#endif
            => new(new(source.Enumerator, second.Enumerator, comparer));

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
   struct Union<TEnumerator, TEnumerator2, TSource>(in TEnumerator source, in TEnumerator2 second, IEqualityComparer<TSource>? comparer)
       : IValueEnumerator<TSource>
       where TEnumerator : struct, IValueEnumerator<TSource>
#if NET9_0_OR_GREATER
        , allows ref struct
#endif
        where TEnumerator2 : struct, IValueEnumerator<TSource>
#if NET9_0_OR_GREATER
        , allows ref struct
#endif
    {
        TEnumerator source = source;
        TEnumerator2 second = second;
        HashSet<TSource>? set;
        byte state = 0;

        public bool TryGetNonEnumeratedCount(out int count)
        {
            count = 0;
            return false;
        }

        public bool TryGetSpan(out ReadOnlySpan<TSource> span)
        {
            span = default;
            return false;
        }

        public bool TryCopyTo(Span<TSource> dest) => false;

        public bool TryGetNext(out TSource current)
        {
            if (state == 0)
            {
                set = new HashSet<TSource>(comparer ?? EqualityComparer<TSource>.Default);
                state = 1;
            }

            if (state == 1)
            {
                while (source.TryGetNext(out var value) && set!.Add(value))
                {
                    current = value;
                    return true;
                }
                state = 2;
            }

            if (state == 2)
            {
                while (second.TryGetNext(out var value) && set!.Add(value))
                {
                    current = value;
                    return true;
                }

                state = 3;
            }

            Unsafe.SkipInit(out current);
            return false;
        }

        public void Dispose()
        {
            state = 3;
            source.Dispose();
            second.Dispose();
        }
    }
}
