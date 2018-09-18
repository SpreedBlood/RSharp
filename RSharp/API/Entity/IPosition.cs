namespace RSharp.API.Entity
{
    public interface IPosition
    {
        /// <summary>
        /// Gets the x coordinate of the position.
        /// </summary>
        int X { get; set; }

        /// <summary>
        /// Gets the y coordinate of the position.
        /// </summary>
        int Y { get; set; }

        /// <summary>
        /// Gets the x coordinate of the map region position.
        /// </summary>
        int MapRegionX { get; }

        /// <summary>
        /// Gets teh y coordinate of the map region position.
        /// </summary>
        int MapRegionY { get; }
    }
}
