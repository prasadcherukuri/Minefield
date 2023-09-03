namespace Minefield.Domain
{
    /// <summary>
    /// Represents a tile on the game board.
    /// </summary>
    public interface ITile
    {
        /// <summary>
        /// Gets or sets a value indicating whether this tile contains a mine.
        /// </summary>
        bool IsMine { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this tile has been visited by the player.
        /// </summary>
        bool IsVisited { get; set; }
    }
}
