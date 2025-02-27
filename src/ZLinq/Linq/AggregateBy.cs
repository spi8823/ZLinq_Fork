﻿//namespace ZLinq
//{
//    partial class ValueEnumerableExtensions
//    {
//        public static AggregateByValueEnumerable<TEnumerable, TSource, TKey, TAccumulate> AggregateBy<TEnumerable, TSource, TKey, TAccumulate>(this TEnumerable source, Func<TSource, TKey> keySelector, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func, IEqualityComparer<TKey> keyComparer)
//            where TEnumerable : struct, IValueEnumerable<TSource>
//#if NET9_0_OR_GREATER
//            , allows ref struct
//#endif
//            => new(source, keySelector, seed, func, keyComparer);

//        public static AggregateByValueEnumerable2<TEnumerable, TSource, TKey, TAccumulate> AggregateBy<TEnumerable, TSource, TKey, TAccumulate>(this TEnumerable source, Func<TSource, TKey> keySelector, Func<TKey, TAccumulate> seedSelector, Func<TAccumulate, TSource, TAccumulate> func, IEqualityComparer<TKey> keyComparer)
//            where TEnumerable : struct, IValueEnumerable<TSource>
//#if NET9_0_OR_GREATER
//            , allows ref struct
//#endif
//            => new(source, keySelector, seedSelector, func, keyComparer);

//    }
//}

//namespace ZLinq.Linq
//{
//    [StructLayout(LayoutKind.Auto)]
//    [EditorBrowsable(EditorBrowsableState.Never)]
//#if NET9_0_OR_GREATER
//    public ref
//#else
//    public
//#endif
//    struct AggregateByValueEnumerable<TEnumerable, TSource, TKey, TAccumulate>(TEnumerable source, Func<TSource, TKey> keySelector, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func, IEqualityComparer<TKey> keyComparer)
//        : IValueEnumerable<KeyValuePair`2>
//        where TEnumerable : struct, IValueEnumerable<TSource>
//#if NET9_0_OR_GREATER
//        , allows ref struct
//#endif
//    {
//        TEnumerable source = source;

//        public ValueEnumerator<AggregateByValueEnumerable<TEnumerable, TSource, TKey, TAccumulate>, KeyValuePair`2> GetEnumerator() => new(this);

//        public bool TryGetNonEnumeratedCount(out int count)
//        {
//            throw new NotImplementedException();
//            // return source.TryGetNonEnumeratedCount(count);
//            // count = 0;
//            // return false;
//        }

//        public bool TryGetSpan(out ReadOnlySpan<KeyValuePair`2> span)
//        {
//            throw new NotImplementedException();
//            // span = default;
//            // return false;
//        }

//        public bool TryGetNext(out KeyValuePair`2 current)
//        {
//            throw new NotImplementedException();
//            // Unsafe.SkipInit(out current);
//            // return false;
//        }

//        public void Dispose()
//        {
//            source.Dispose();
//        }
//    }

//    [StructLayout(LayoutKind.Auto)]
//    [EditorBrowsable(EditorBrowsableState.Never)]
//#if NET9_0_OR_GREATER
//    public ref
//#else
//    public
//#endif
//    struct AggregateByValueEnumerable2<TEnumerable, TSource, TKey, TAccumulate>(TEnumerable source, Func<TSource, TKey> keySelector, Func<TKey, TAccumulate> seedSelector, Func<TAccumulate, TSource, TAccumulate> func, IEqualityComparer<TKey> keyComparer)
//        : IValueEnumerable<KeyValuePair`2>
//        where TEnumerable : struct, IValueEnumerable<TSource>
//#if NET9_0_OR_GREATER
//        , allows ref struct
//#endif
//    {
//        TEnumerable source = source;

//        public ValueEnumerator<AggregateByValueEnumerable2<TEnumerable, TSource, TKey, TAccumulate>, KeyValuePair`2> GetEnumerator() => new(this);

//        public bool TryGetNonEnumeratedCount(out int count)
//        {
//            throw new NotImplementedException();
//            // return source.TryGetNonEnumeratedCount(count);
//            // count = 0;
//            // return false;
//        }

//        public bool TryGetSpan(out ReadOnlySpan<KeyValuePair`2> span)
//        {
//            throw new NotImplementedException();
//            // span = default;
//            // return false;
//        }

//        public bool TryGetNext(out KeyValuePair`2 current)
//        {
//            throw new NotImplementedException();
//            // Unsafe.SkipInit(out current);
//            // return false;
//        }

//        public void Dispose()
//        {
//            source.Dispose();
//        }
//    }

//}
