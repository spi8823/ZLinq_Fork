﻿namespace ZLinq
{
    partial class ValueEnumerableExtensions
    {
        public static TSource First<TEnumerator, TSource>(in this ValueEnumerable<TEnumerator, TSource> source)
            where TEnumerator : struct, IValueEnumerator<TSource>
#if NET9_0_OR_GREATER
            , allows ref struct
#endif
        {
            var enumerator = source.Enumerator;
            try
            {
                return TryGetFirst<TEnumerator, TSource>(ref enumerator, out var value)
                    ? value
                    : Throws.NoElements<TSource>();
            }
            finally
            {
                enumerator.Dispose();
            }
        }

        public static TSource First<TEnumerator, TSource>(in this ValueEnumerable<TEnumerator, TSource> source, Func<TSource, Boolean> predicate)
            where TEnumerator : struct, IValueEnumerator<TSource>
#if NET9_0_OR_GREATER
            , allows ref struct
#endif
        {
            var enumerator = source.Enumerator;
            try
            {
                return TryGetFirst<TEnumerator, TSource>(ref enumerator, predicate, out var value)
                ? value
                : Throws.NoMatch<TSource>();
            }
            finally
            {
                enumerator.Dispose();
            }
        }

        public static TSource? FirstOrDefault<TEnumerator, TSource>(in this ValueEnumerable<TEnumerator, TSource> source)
            where TEnumerator : struct, IValueEnumerator<TSource>
#if NET9_0_OR_GREATER
            , allows ref struct
#endif
        {
            var enumerator = source.Enumerator;
            try
            {
                return TryGetFirst<TEnumerator, TSource>(ref enumerator, out var value)
                ? value
                : default;
            }
            finally
            {
                enumerator.Dispose();
            }
        }

        public static TSource FirstOrDefault<TEnumerator, TSource>(in this ValueEnumerable<TEnumerator, TSource> source, TSource defaultValue)
            where TEnumerator : struct, IValueEnumerator<TSource>
#if NET9_0_OR_GREATER
            , allows ref struct
#endif
        {
            var enumerator = source.Enumerator;
            try
            {
                return TryGetFirst<TEnumerator, TSource>(ref enumerator, out var value)
                ? value
                : defaultValue;
            }
            finally
            {
                enumerator.Dispose();
            }
        }

        public static TSource? FirstOrDefault<TEnumerator, TSource>(in this ValueEnumerable<TEnumerator, TSource> source, Func<TSource, Boolean> predicate)
            where TEnumerator : struct, IValueEnumerator<TSource>
#if NET9_0_OR_GREATER
            , allows ref struct
#endif
        {
            var enumerator = source.Enumerator;
            try
            {
                return TryGetFirst<TEnumerator, TSource>(ref enumerator, predicate, out var value)
                ? value
                : default;
            }
            finally
            {
                enumerator.Dispose();
            }
        }

        public static TSource FirstOrDefault<TEnumerator, TSource>(in this ValueEnumerable<TEnumerator, TSource> source, Func<TSource, Boolean> predicate, TSource defaultValue)
            where TEnumerator : struct, IValueEnumerator<TSource>
#if NET9_0_OR_GREATER
            , allows ref struct
#endif
        {
            var enumerator = source.Enumerator;
            try
            {
                return TryGetFirst<TEnumerator, TSource>(ref enumerator, predicate, out var value)
                ? value
                : defaultValue;
            }
            finally
            {
                enumerator.Dispose();
            }
        }

        static bool TryGetFirst<TEnumerator, TSource>(ref TEnumerator source, out TSource value)
            where TEnumerator : struct, IValueEnumerator<TSource>
#if NET9_0_OR_GREATER
            , allows ref struct
#endif
        {
            if (source.TryGetSpan(out var span))
            {
                if (span.Length > 0)
                {
                    value = span[0];
                    return true;
                }

                value = default!;
                return false;
            }

            if (source.TryGetNext(out value))
            {
                return true;
            }

            value = default!;
            return false;
        }

        static bool TryGetFirst<TEnumerator, TSource>(ref TEnumerator source, Func<TSource, Boolean> predicate, out TSource value)
            where TEnumerator : struct, IValueEnumerator<TSource>
#if NET9_0_OR_GREATER
            , allows ref struct
#endif
        {
            if (source.TryGetSpan(out var span))
            {
                foreach (var item in span)
                {
                    if (predicate(item))
                    {
                        value = item;
                        return true;
                    }
                }

                value = default!;
                return false;
            }

            while (source.TryGetNext(out var current))
            {
                if (predicate(current))
                {
                    value = current;
                    return true;
                }
            }

            value = default!;
            return false;
        }
    }
}
