namespace ImageCompare.DTOs
{
    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Position()
        {
        }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool NextTo(Position p, int distance)
        {
            return X < p.X + distance && X > p.X - distance
                && Y < p.Y + distance && Y > p.Y - distance;
        }
    }
}
