using System.Numerics;

namespace PageOfBob.Advent2022
{
    // Why write my own vector class? Why not!?
    public record struct Vector2<T>(T X, T Y) where T : INumber<T>
    {
        public Vector2<T> Translate(Vector2<T> vector)
            => new Vector2<T>(vector.X + X, vector.Y + Y);

        public Vector2<T> Difference(Vector2<T> vector)
            => new Vector2<T>(vector.X - X, vector.Y - Y);
        public Vector2<T> Apply(Func<T, T> func)
            => new Vector2<T>(func(X), func(Y));

        public bool BothMatch(Func<T, bool> func)
            => func(X) && func(Y);
    }

    public static class Vectors
    {
        public static readonly Vector2<int> North = new Vector2<int>(0, -1);
        public static readonly Vector2<int> East = new Vector2<int>(1, 0);
        public static readonly Vector2<int> South = new Vector2<int>(0, 1);
        public static readonly Vector2<int> West = new Vector2<int>(-1, 0);

        public static readonly Vector2<int>[] CardinalDirections = new[] { North, East, South, West };

        public static Vector2<T> Abs<T>(this Vector2<T> vector) where T : INumber<T>, ISignedNumber<T>
            => vector.Apply(x => x < T.Zero ? -x : x);
    }
}
