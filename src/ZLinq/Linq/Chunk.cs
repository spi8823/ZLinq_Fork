﻿namespace ZLinq
{
    partial class ValueEnumerableExtensions
    {
        public static ValueEnumerable<Chunk<TEnumerator, TSource>, TSource[]> Chunk<TEnumerator, TSource>(in this ValueEnumerable<TEnumerator, TSource> source, Int32 size)
            where TEnumerator : struct, IValueEnumerator<TSource>
#if NET9_0_OR_GREATER
            , allows ref struct
#endif
        {
            if (size < 1) Throws.ArgumentOutOfRange(nameof(size));

            return new(new(source.Enumerator, size));
        }
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
    struct Chunk<TEnumerator, TSource>(in TEnumerator source, Int32 size)
        : IValueEnumerator<TSource[]>
        where TEnumerator : struct, IValueEnumerator<TSource>
#if NET9_0_OR_GREATER
        , allows ref struct
#endif
    {
        TEnumerator source = source;
        int index;
#if NET9_0_OR_GREATER
        ReadOnlySpan<TSource> sourceSpan;
#endif
        // 3byte(Considering the padding, this is good in terms of memory.)
        bool isInitialized;
        bool isCompleted;
        bool isCanGetSpan;

        public bool TryGetNonEnumeratedCount(out int count)
        {
            if (source.TryGetNonEnumeratedCount(out var sourceCount))
            {
                count = (sourceCount + size - 1) / size; // Calculate the total number of chunks
                return true;
            }

            count = 0;
            return false;
        }

        public bool TryGetSpan(out ReadOnlySpan<TSource[]> span)
        {
            span = default;
            return false;
        }

        public bool TryCopyTo(Span<TSource[]> dest) => false;

        public bool TryGetNext(out TSource[] current)
        {
            if (isCompleted)
            {
                current = default!;
                return false;
            }

            if (!isInitialized)
            {
                isInitialized = true;

                if (source.TryGetSpan(out var span))
                {
                    isCanGetSpan = true;
#if NET9_0_OR_GREATER
                    sourceSpan = span;
#endif
                    if (span.Length == 0)
                    {
                        isCompleted = true;
                        current = default!;
                        return false;
                    }
                }
            }

            if (isCanGetSpan)
            {
#if NET9_0_OR_GREATER
                var span = sourceSpan;
#else
                source.TryGetSpan(out var span);
#endif

                var currentSpan = span.Slice(index, Math.Min(size, span.Length - index));
                index += currentSpan.Length;
                current = currentSpan.ToArray();
                if (index == span.Length)
                {
                    isCompleted = true;
                }
                return true;
            }
            else // noSpan
            {
                index = 0;
                current = null!;
                while (source.TryGetNext(out var value))
                {
                    if (current == null) current = new TSource[size];

                    current[index++] = value;
                    if (index == size)
                    {
                        index = 0;
                        return true;
                    }
                }

                isCompleted = true;
                if (current == null) return false;
                if (current.Length != index)
                {
                    Array.Resize(ref current, index);
                }
                return true;
            }
        }

        public void Dispose()
        {
            source.Dispose();
        }
    }

}
