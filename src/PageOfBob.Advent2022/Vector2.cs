using System.Numerics;

namespace PageOfBob.Advent2022
{
    // Why write my own vector class? Why not!?
    public record Vector2<T>(T X, T Y) where T : INumber<T>
    {
        public Vector2<T> Translate(Vector2<T> vector)
            => new Vector2<T>(vector.X + X, vector.Y + Y);
    }

    public static class Vectors
    {
        public static readonly Vector2<int> North = new Vector2<int>(0, -1);
        public static readonly Vector2<int> East = new Vector2<int>(1, 0);
        public static readonly Vector2<int> South = new Vector2<int>(0, 1);
        public static readonly Vector2<int> West = new Vector2<int>(-1, 0);

        public static readonly Vector2<int>[] CardinalDirections = new[] { North, East, South, West };
    }
}
