using Minefield.Domain;

namespace Minefield.Core
{
    /// <summary>
    /// Provides game logic for updating the game state.
    /// </summary>
    public class GameManager
    {
        /// <summary>
        /// Updates the game state based on the player's input direction.
        /// </summary>
        /// <param name="chessboard">The chessboard on which the game is played.</param>
        /// <param name="player">The player object representing the player's state.</param>
        /// <param name="direction">The direction in which the player wants to move (up, down, left, or right).</param>
        /// <returns>The current game state after updating.</returns>
        public static GameState UpdateGameState(IChessboard chessboard, IPlayer player, string direction)
        {
            int currentRow = player.CurrentRow;
            int currentColumn = player.CurrentColumn;

            // Mark the current tile as visited before moving
            chessboard.GetTile(currentRow, currentColumn).IsVisited = true;

            switch (direction)
            {
                case "up":
                    TryMoveUp(chessboard, player, ref currentRow);
                    break;
                case "down":
                    TryMoveDown(chessboard, player, ref currentRow);
                    break;
                case "left":
                    TryMoveLeft(chessboard, player, ref currentColumn);
                    break;
                case "right":
                    TryMoveRight(chessboard, player, ref currentColumn);
                    break;
                default:
                    return HandleInvalidInput();
            }

            // Update the player's position
            player.CurrentRow = currentRow;
            player.CurrentColumn = currentColumn;

            Tile newTile = chessboard.GetTile(currentRow, currentColumn);
            if (newTile.IsMine)
            {
                return HandleMineHit(player);
            }

            return HandleGameProgress(chessboard, player, currentRow, currentColumn);
        }

        /// <summary>
        /// Attempts to move the player up and increments the move count if successful.
        /// </summary>
        /// <param name="chessboard">The chessboard on which the game is played.</param>
        /// <param name="player">The player object representing the player's state.</param>
        /// <param name="currentRow">The current row of the player's position.</param>
        private static void TryMoveUp(IChessboard chessboard, IPlayer player, ref int currentRow)
        {
            if (currentRow > 0)
            {
                currentRow--;
                player.IncrementMoves();
            }
        }

        /// <summary>
        /// Attempts to move the player down and increments the move count if successful.
        /// </summary>
        /// <param name="chessboard">The chessboard on which the game is played.</param>
        /// <param name="player">The player object representing the player's state.</param>
        /// <param name="currentRow">The current row of the player's position.</param>
        private static void TryMoveDown(IChessboard chessboard, IPlayer player, ref int currentRow)
        {
            if (currentRow < chessboard.Rows - 1)
            {
                currentRow++;
                player.IncrementMoves();
            }
        }

        /// <summary>
        /// Attempts to move the player left and increments the move count if successful.
        /// </summary>
        /// <param name="chessboard">The chessboard on which the game is played.</param>
        /// <param name="player">The player object representing the player's state.</param>
        /// <param name="currentColumn">The current column of the player's position.</param>
        private static void TryMoveLeft(IChessboard chessboard, IPlayer player, ref int currentColumn)
        {
            if (currentColumn > 0)
            {
                currentColumn--;
                player.IncrementMoves();
            }
        }

        /// <summary>
        /// Attempts to move the player right and increments the move count if successful.
        /// </summary>
        /// <param name="chessboard">The chessboard on which the game is played.</param>
        /// <param name="player">The player object representing the player's state.</param>
        /// <param name="currentColumn">The current column of the player's position.</param>
        private static void TryMoveRight(IChessboard chessboard, IPlayer player, ref int currentColumn)
        {
            if (currentColumn < chessboard.Columns - 1)
            {
                currentColumn++;
                player.IncrementMoves();
            }
        }

        /// <summary>
        /// Handles the game state when the user provides invalid input direction.
        /// </summary>
        /// <returns>The game state indicating invalid user input.</returns>
        private static GameState HandleInvalidInput()
        {
            return GameState.InvalidUserInput;
        }

        /// <summary>
        /// Handles the game state when the player hits a mine, decrements lives, and checks for game over conditions.
        /// </summary>
        /// <param name="player">The player object representing the player's state.</param>
        /// <returns>The updated game state based on the player's condition.</returns>
        private static GameState HandleMineHit(IPlayer player)
        {
            player.DecrementLives();
            return player.Lives <= 0 ? GameState.Lost : GameState.LifeLost;
        }

        /// <summary>
        /// Handles the game state when the game is in progress and checks for win conditions.
        /// </summary>
        /// <param name="chessboard">The chessboard on which the game is played.</param>
        /// <param name="player">The player object representing the player's state.</param>
        /// <param name="currentRow">The current row of the player's position.</param>
        /// <param name="currentColumn">The current column of the player's position.</param>
        /// <returns>The updated game state based on the player's progress.</returns>
        private static GameState HandleGameProgress(IChessboard chessboard, IPlayer player, int currentRow, int currentColumn)
        {
            if (currentColumn == chessboard.Columns - 1 || currentRow == chessboard.Rows - 1)
            {
                return GameState.Won;
            }

            return GameState.Inprogress;
        }
    }
}
