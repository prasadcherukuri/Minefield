namespace Minefield.Domain
{
    /// <summary>
    /// Represents a player in the game.
    /// </summary>
    public interface IPlayer
    {
        /// <summary>
        /// Gets or sets the current column position of the player.
        /// </summary>
        int CurrentColumn { get; set; }

        /// <summary>
        /// Gets or sets the current row position of the player.
        /// </summary>
        int CurrentRow { get; set; }

        /// <summary>
        /// Gets the current position of the player in the chess board format "ex. A1, B3, C4."
        /// </summary>
        string CurrentPosition { get; }

        /// <summary>
        /// Gets the remaining lives of the player.
        /// </summary>
        int Lives { get; }

        /// <summary>
        /// Gets the number of moves made by the player.
        /// </summary>
        int Moves { get; }

        /// <summary>
        /// Gets the starting position of the player in the chess board format "ex. A1, B3, C4."
        /// </summary>
        string StartingPosition { get; }

        /// <summary>
        /// Calculates the final score of the player.
        /// </summary>
        /// <returns>The final score based on the player's moves.</returns>
        int CalculateFinalScore();

        /// <summary>
        /// Decrements the player's lives by 1.
        /// </summary>
        void DecrementLives();

        /// <summary>
        /// Increments the number of moves made by the player by 1.
        /// </summary>
        void IncrementMoves();
    }
}
