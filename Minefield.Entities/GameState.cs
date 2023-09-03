namespace Minefield.Domain
{
    /// <summary>
    /// Represents the possible states of the Minefield game.
    /// </summary>
    public enum GameState
    {
        /// <summary>
        /// The game is in a new state, ready to start.
        /// </summary>
        New,

        /// <summary>
        /// The game is in progress.
        /// </summary>
        Inprogress,

        /// <summary>
        /// The player has won the game.
        /// </summary>
        Won,

        /// <summary>
        /// The player has lost the game.
        /// </summary>
        Lost,

        /// <summary>
        /// The player has lost a life.
        /// </summary>
        LifeLost,

        /// <summary>
        /// The player has provided an invalid user input.
        /// </summary>
        InvalidUserInput
    }
}
