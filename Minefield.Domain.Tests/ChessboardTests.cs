namespace Minefield.Domain.Tests
{
    public class ChessboardTests
    {
        [Fact]
        public void Chessboard_Constructor_DefaultDimensions()
        {
            // Arrange & Act
            Chessboard chessboard = new Chessboard(new Player());

            // Assert
            Assert.Equal(8, chessboard.Rows);
            Assert.Equal(8, chessboard.Columns);
        }

        [Fact]
        public void Chessboard_Constructor_CustomDimensions()
        {
            // Arrange & Act
            Chessboard chessboard = new Chessboard(new Player(), rows: 10, columns: 12);

            // Assert
            Assert.Equal(10, chessboard.Rows);
            Assert.Equal(12, chessboard.Columns);
        }

        [Fact]
        public void GetTile_ValidPosition()
        {
            // Arrange
            Chessboard chessboard = new Chessboard(new Player());

            // Act
            Tile tile = chessboard.GetTile(0, 0);

            // Assert
            Assert.NotNull(tile);
        }

        [Fact]
        public void GetTile_OutOfBoundsPosition()
        {
            // Arrange
            Chessboard chessboard = new Chessboard(new Player());

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => chessboard.GetTile(-1, 0));
            Assert.Throws<ArgumentOutOfRangeException>(() => chessboard.GetTile(0, -1));
            Assert.Throws<ArgumentOutOfRangeException>(() => chessboard.GetTile(8, 0));
            Assert.Throws<ArgumentOutOfRangeException>(() => chessboard.GetTile(0, 8));
        }

        [Fact]
        public void PlaceMines_DefaultNumberOfMines()
        {
            // Arrange
            Chessboard chessboard = new Chessboard(new Player());

            // Act
            // By default 40 mines are placed
            chessboard.PlaceMines();

            // Assert
            // Check that 40 mines are placed.
            int mineCount = 0;
            for (int row = 0; row < chessboard.Rows; row++)
            {
                for (int col = 0; col < chessboard.Columns; col++)
                {
                    if (chessboard.GetTile(row, col).IsMine)
                    {
                        mineCount++;
                    }
                }
            }

            Assert.Equal(40, mineCount);
        }

        [Fact]
        public void PlaceMines_CustomNumberOfMines()
        {
            // Arrange
            Chessboard chessboard = new Chessboard(new Player());

            // Act
            chessboard.PlaceMines(numberOfMines: 10);

            // Assert
            // Check that exactly 10 mines are placed, you can use your own logic to count the mines.
            int mineCount = 0;
            for (int row = 0; row < chessboard.Rows; row++)
            {
                for (int col = 0; col < chessboard.Columns; col++)
                {
                    if (chessboard.GetTile(row, col).IsMine)
                    {
                        mineCount++;
                    }
                }
            }
            Assert.Equal(10, mineCount);
        }
    }
}
