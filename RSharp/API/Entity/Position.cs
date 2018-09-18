namespace RSharp.API.Entity
{
    public class Position : IPosition
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int MapRegionX => (X >> 3) - 6;
        public int MapRegionY => (Y >> 3) - 6;

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
