using System.Numerics;

namespace PageOfBob.Advent2022
{
    public record Range<T>(T Start, T End) where T : INumber<T>
    {
        public Range(SplitTwo<T> split) : this(split.Left, split.Right) { }

        public bool Contains(Range<T> other) => Start <= other.Start && End >= other.End;

        public bool Overlaps(Range<T> other)
        {
            var maxStart = Start > other.Start ? Start : other.Start;
            var minEnd = End < other.End ? End : other.End;
            return maxStart <= minEnd;
        }
    }
}
