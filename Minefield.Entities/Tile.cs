namespace Minefield.Domain
{
    /// <summary>
    /// Represents a tile on the game board.
    /// </summary>
    public class Tile : ITile
    {
        /// <summary>
        /// Gets or sets a value indicating whether this tile contains a mine.
        /// </summary>
        /// <value>True if this tile contains a mine, otherwise false.</value>
        public bool IsMine { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this tile has been visited by the player.
        /// </summary>
        /// <value>True if this tile has been visited, otherwise false.</value>
        public bool IsVisited { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Tile"/> class with specified mine and visited status.
        /// </summary>
        /// <param name="isMine">True if this tile contains a mine, otherwise false.</param>
        /// <param name="isVisited">True if this tile has been visited, otherwise false.</param>
        public Tile(bool isMine, bool isVisited)
        {
            IsMine = isMine;
            IsVisited = isVisited;
        }
    }
}
