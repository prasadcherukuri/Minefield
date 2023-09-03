using Minefield.Domain;
/// <summary>
/// Represents a chessboard interface with properties and methods for managing game tiles and mines.
/// </summary>
public interface IChessboard
{
    /// <summary>
    /// Gets the number of columns on the chessboard.
    /// </summary>
    int Columns { get; }

    /// <summary>
    /// Gets the number of rows on the chessboard.
    /// </summary>
    int Rows { get; }

    /// <summary>
    /// Retrieves the tile at the specified row and column indices.
    /// </summary>
    /// <param name="row">The row index (0 to Rows-1).</param>
    /// <param name="column">The column index (0 to Columns-1).</param>
    /// <returns>The tile at the specified position.</returns>
    Tile GetTile(int row, int column);

    /// <summary>
    /// Places mines on the chessboard, with an optional number of mines.
    /// </summary>
    /// <param name="numberOfMines">The number of mines to place on the chessboard (default is 20).</param>
    void PlaceMines(int numberOfMines = 20);
}
