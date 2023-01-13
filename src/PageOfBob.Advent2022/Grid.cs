namespace PageOfBob.Advent2022
{
    public interface IGrid<T>
    {
        T? GetAt(Vector2<int> point);
        int Width { get; }
        int Height { get; }
    }

    public record Grid<T>(IList<T> Data, int Width, int Height) : IGrid<T>
    {
        public static Grid<T> Create(string input, Func<char, T> transform)
        {
            var lines = input.SplitOnNewlines().Where(x => !string.IsNullOrEmpty(x)).ToArray();
            var width = lines[0].Length;
            var height = lines.Length;

            var data = new List<T>(width * height);
            int index = 0;
            foreach (var line in lines)
            {
                foreach (var c in line)
                {
                    data.Add(transform(c));
                    index++;
                }
            }

            return new Grid<T>(data, width, height);
        }

        public T GetAt(Vector2<int> point)
            => Data[point.Y * Width + point.X];
    }

    public record GridPoint<T>(Vector2<int> Point, T Value);

    public static class GridExtensions
    {
        public static IEnumerable<Vector2<int>> AllPoints<T>(this IGrid<T> grid)
            => Enumerable.Range(0, grid.Height).SelectMany(y => Enumerable.Range(0, grid.Height).Select(x => new Vector2<int>(x, y)));

        public static IEnumerable<GridPoint<T>> AllPointsWithValues<T>(this IGrid<T> grid)
            => AllPoints(grid).Select(pt => new GridPoint<T>(pt, grid.GetAt(pt)));

        public static bool IsWithinGrid<T>(this IGrid<T> grid, Vector2<int> point)
            => point.X >= 0 && point.Y >= 0 && point.X < grid.Width && point.Y < grid.Height;


        public static IEnumerable<T> ListPointsInDirection<T>(this IGrid<T> grid, Vector2<int> position, Vector2<int> direction)
        {
            var nextPoint = position.Translate(direction);
            while (grid.IsWithinGrid(nextPoint))
            {
                yield return grid.GetAt(nextPoint);
                nextPoint = nextPoint.Translate(direction);
            }
        }
    }
}
