namespace Minefield.Domain
{
    /// <summary>
    /// Represents a player in the game.
    /// </summary>
    public class Player : IPlayer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class with optional initial lives and starting position.
        /// </summary>
        /// <param name="initialLives">The initial number of lives for the player (default is 3).</param>
        /// <param name="startingPosition">The starting position of the player in chess notation (e.g., 'A1').</param>
        public Player(int initialLives = 3, string startingPosition = "A1")
        {
            Lives = initialLives;
            Moves = 0;
            (CurrentRow, CurrentColumn) = ConvertFromChessPosition(startingPosition);
            StartingPosition = startingPosition;
            CurrentPosition = startingPosition;
        }

        /// <summary>
        /// Gets the number of lives remaining for the player.
        /// </summary>
        public int Lives { get; private set; }

        /// <summary>
        /// Gets the number of moves made by the player.
        /// </summary>
        public int Moves { get; private set; }

        /// <summary>
        /// Gets the starting position of the player in chess notation (e.g., 'A1').
        /// </summary>
        public string StartingPosition { get; private set; }

        // <summary>
        /// Gets the current position of the player in chess notation (e.g., 'A1').
        /// </summary>
        public string CurrentPosition { get; private set; }

        private int currentRow;

        /// <summary>
        /// Gets or sets the current row position of the player.
        /// </summary>
        public int CurrentRow
        {
            get
            {
                return currentRow;
            }
            set
            {
                if (value >= 0 || value <= 7)
                {
                    currentRow = value;
                    CurrentPosition = ConvertToChessPosition(value, CurrentColumn);
                }
                else
                {
                    throw new ArgumentOutOfRangeException("CurrentRow must be a value between 0 and 7 inclusive");
                }
            }
        }

        private int currentColumn;

        /// <summary>
        /// Gets or sets the current column position of the player.
        /// </summary>
        public int CurrentColumn
        {
            get
            {
                return currentColumn;
            }
            set
            {
                if (value >= 0 || value <= 7)
                {
                    currentColumn = value;
                    CurrentPosition = ConvertToChessPosition(currentRow, value);
                }
                else
                {
                    throw new ArgumentOutOfRangeException("CurrentColumn must be a value between 0 and 7 inclusive");
                }
            }
        }

        /// <summary>
        /// Increments the number of moves made by the player.
        /// </summary>
        public void IncrementMoves()
        {
            Moves++;
        }

        /// <summary>
        /// Decrements the player's lives by 1.
        /// </summary>
        public void DecrementLives()
        {
            Lives--;
        }

        /// <summary>
        /// Calculates the final score of the player based on the number of moves taken.
        /// </summary>
        /// <returns>The final score as an integer.</returns>
        public int CalculateFinalScore()
        {
            return Moves;
        }

        /// <summary>
        /// Converts row and column indices into a chessboard position.
        /// </summary>
        /// <param name="row">The row index (0 to 7).</param>
        /// <param name="column">The column index (0 to 7).</param>
        /// <returns>A string representing the chessboard position (e.g., "A1").</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if row or column values are out of bounds.</exception>
        internal static string ConvertToChessPosition(int row, int column)
        {
            if (row < 0 || row > 7 || column < 0 || column > 7)
            {
                throw new ArgumentOutOfRangeException("Row and column values must be between 0 and 7.");
            }

            char file = (char)('A' + column);
            string position = $"{file}{row + 1}";

            return position;
        }

        /// <summary>
        /// Converts a chessboard position (e.g., "A1") into row and column indices.
        /// </summary>
        /// <param name="chessPosition">The chessboard position to convert (e.g., "A1").</param>
        /// <returns>A tuple containing the row and column indices.</returns>
        /// <exception cref="ArgumentException">Thrown if the input position is in an invalid format or out of bounds.</exception>
        internal static (int row, int column) ConvertFromChessPosition(string chessPosition)
        {
            if (string.IsNullOrEmpty(chessPosition) || chessPosition.Length != 2)
            {
                throw new ArgumentException("Invalid chess position format. Use two characters, e.g., 'A1'.");
            }

            char fileChar = char.ToUpper(chessPosition[0]);
            char rowChar = chessPosition[1];

            if (fileChar < 'A' || fileChar > 'H' || rowChar < '1' || rowChar > '8')
            {
                throw new ArgumentException("Invalid chess position. Valid positions are from 'A1' to 'H8'.");
            }

            int column = fileChar - 'A';
            int row = rowChar - '1';

            return (row, column);
        }
    }
}
