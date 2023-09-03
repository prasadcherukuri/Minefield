namespace Minefield.Domain
{
    /// <summary>
    /// Represents a chessboard for the Minefield game with tiles and mines.
    /// </summary>
    public class Chessboard : IChessboard
    {
        private Tile[,] board;
        private readonly IPlayer player;

        /// <summary>
        /// Gets the number of rows on the chessboard.
        /// </summary>
        public int Rows { get; }

        /// <summary>
        /// Gets the number of columns on the chessboard.
        /// </summary>
        public int Columns { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Chessboard"/> class with the specified player and optional board dimensions.
        /// </summary>
        /// <param name="player">The player for the game.</param>
        /// <param name="rows">The number of rows on the chessboard (optional and the default is 8).</param>
        /// <param name="columns">The number of columns on the chessboard (optional and the default is 8).</param>
        public Chessboard(IPlayer player, int rows = 8, int columns = 8)
        {
            this.player = player;
            Rows = rows;
            Columns = columns;
            board = new Tile[rows, columns];

            // Populate the board with Tile objects
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < columns; col++)
                {
                    board[row, col] = new Tile(isMine: false, isVisited: false);
                }
            }
        }

        /// <summary>
        /// Retrieves the tile at the specified row and column indices.
        /// </summary>
        /// <param name="row">The row index (0 to Rows-1).</param>
        /// <param name="column">The column index (0 to Columns-1).</param>
        /// <returns>The tile at the specified position.</returns>
        public Tile GetTile(int row, int column)
        {
            // Ensure that the provided row and column are within bounds
            if (row >= 0 && row < Rows && column >= 0 && column < Columns)
            {
                return board[row, column]; // Return the Tile object at the specified position
            }
            else
            {
                throw new ArgumentOutOfRangeException("Invalid row or column.");
            }
        }

        /// <summary>
        /// Places mines on the chessboard, with an optional number of mines.
        /// </summary>
        /// <param name="numberOfMines">The number of mines to place on the chessboard (default is 40).</param>
        public void PlaceMines(int numberOfMines = 40)
        {
            if (numberOfMines >= Rows * Columns)
            {
                throw new ArgumentException($"Number of mines ({numberOfMines}) exceeds the size of the chessboard ({Rows * Columns}).");
            }

            Random random = new Random();

            // Create a list of unique positions to place mines
            HashSet<(int, int)> minePositions = new HashSet<(int, int)>();

            // Generate random mine positions
            while (minePositions.Count < numberOfMines)
            {
                int row = random.Next(0, Rows);
                int col = random.Next(0, Columns);

                // Ensure that the generated position is unique and not placing a mine at the player's starting position
                if (!minePositions.Contains((row, col)) && (row != this.player.CurrentRow || col != this.player.CurrentColumn))
                {
                    minePositions.Add((row, col));
                    board[row, col].IsMine = true; // Set the tile as a mine
                }
            }
        }
    }
}
