namespace Minefield.Domain.Tests
{
    public class PlayerTests
    {
        [Fact]
        public void Player_Constructor_DefaultValues()
        {
            // Arrange & Act
            Player player = new Player();

            // Assert
            Assert.Equal(3, player.Lives);
            Assert.Equal(0, player.Moves);
            Assert.Equal("A1", player.StartingPosition);
            Assert.Equal("A1", player.CurrentPosition);
            Assert.Equal(0, player.CurrentRow);
            Assert.Equal(0, player.CurrentColumn);
        }

        [Fact]
        public void Player_Constructor_CustomValues()
        {
            // Arrange & Act
            Player player = new Player(initialLives: 5, startingPosition: "C4");

            // Assert
            Assert.Equal(5, player.Lives);
            Assert.Equal(0, player.Moves);
            Assert.Equal("C4", player.StartingPosition);
            Assert.Equal("C4", player.CurrentPosition);
            Assert.Equal(3, player.CurrentRow);
            Assert.Equal(2, player.CurrentColumn);
        }

        [Fact]
        public void IncrementMoves()
        {
            // Arrange
            Player player = new Player();

            // Act
            player.IncrementMoves();

            // Assert
            Assert.Equal(1, player.Moves);
        }

        [Fact]
        public void DecrementLives()
        {
            // Arrange
            Player player = new Player(initialLives: 3);

            // Act
            player.DecrementLives();

            // Assert
            Assert.Equal(2, player.Lives);
        }

        [Fact]
        public void CalculateFinalScore()
        {
            // Arrange
            Player player = new Player();

            // Act
            player.IncrementMoves();
            player.IncrementMoves();
            player.IncrementMoves();
            int finalScore = player.CalculateFinalScore();

            // Assert
            Assert.Equal(3, finalScore);
        }

        [Theory]
        [InlineData(0, 0, "A1")]
        [InlineData(1, 3, "D2")]
        [InlineData(7, 7, "H8")]
        public void ConvertToChessPosition(int row, int column, string expectedPosition)
        {
            // Act
            string position = Player.ConvertToChessPosition(row, column);

            // Assert
            Assert.Equal(expectedPosition, position);
        }

        [Theory]
        [InlineData("A1", 0, 0)]
        [InlineData("D2", 1, 3)]
        [InlineData("H8", 7, 7)]
        public void ConvertFromChessPosition(string chessPosition, int expectedRow, int expectedColumn)
        {
            // Act
            (int row, int column) = Player.ConvertFromChessPosition(chessPosition);

            // Assert
            Assert.Equal(expectedRow, row);
            Assert.Equal(expectedColumn, column);
        }

        [Theory]
        [InlineData("A0")]
        [InlineData("A9")]
        [InlineData("I1")]
        [InlineData("A")]
        [InlineData("")]
        public void ConvertFromChessPosition_InvalidPosition(string chessPosition)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => Player.ConvertFromChessPosition(chessPosition));
        }

        [Theory]
        [InlineData("A10")]
        [InlineData("K1")]
        public void ConvertFromChessPosition_OutOfBoundsPosition(string chessPosition)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => Player.ConvertFromChessPosition(chessPosition));
        }
    }
}
