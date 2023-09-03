using Minefield.Domain;

namespace Minefield.Core.Tests
{
    public class GameManagerTests
    {
        [Fact]
        public void UpdateGameState_InvalidInput_ReturnsInvalidUserInput()
        {
            // Arrange
            IPlayer player = new Player();
            IChessboard chessboard = new Chessboard(player);
            string invalidDirection = "invalid";

            // Act
            GameState result = GameManager.UpdateGameState(chessboard, player, invalidDirection);

            // Assert
            Assert.Equal(GameState.InvalidUserInput, result);
        }

        [Fact]
        public void UpdateGameState_MoveUpWithinBounds_InitialPosition_0_0_DoesNot_IncrementsMoves()
        {
            // Arrange
            IPlayer player = new Player();
            IChessboard chessboard = new Chessboard(player);
            string direction = "up";
            int initialRow = player.CurrentRow;

            // Act
            GameManager.UpdateGameState(chessboard, player, direction);

            // Assert
            Assert.Equal(initialRow, player.CurrentRow);
            Assert.Equal(0, player.Moves);
        }

        [Fact]
        public void UpdateGameState_MoveDownWithinBounds_IncrementsMoves()
        {
            // Arrange
            IPlayer player = new Player();
            IChessboard chessboard = new Chessboard(player);
            string direction = "down";
            int initialRow = player.CurrentRow;

            // Act
            GameManager.UpdateGameState(chessboard, player, direction);

            // Assert
            Assert.Equal(initialRow + 1, player.CurrentRow);
            Assert.Equal(1, player.Moves);
        }

        [Fact]
        public void UpdateGameState_MoveLeftWithinBounds_InitialPosition_0_0_DoesNotIncrementsMoves()
        {
            // Arrange
            IPlayer player = new Player();
            IChessboard chessboard = new Chessboard(player);
            string direction = "left";
            int initialColumn = player.CurrentColumn;

            // Act
            GameManager.UpdateGameState(chessboard, player, direction);

            // Assert
            Assert.Equal(initialColumn, player.CurrentColumn);
            Assert.Equal(0, player.Moves);
        }

        [Fact]
        public void UpdateGameState_MoveRightWithinBounds_IncrementsMoves()
        {
            // Arrange
            IPlayer player = new Player();
            IChessboard chessboard = new Chessboard(player);
            string direction = "right";
            int initialColumn = player.CurrentColumn;

            // Act
            GameManager.UpdateGameState(chessboard, player, direction);

            // Assert
            Assert.Equal(initialColumn + 1, player.CurrentColumn);
            Assert.Equal(1, player.Moves);
        }

        [Fact]
        public void UpdateGameState_HitMine_DecrementsLives()
        {
            // Arrange
            IPlayer player = new Player();
            IChessboard chessboard = new Chessboard(player, rows: 2, columns: 2);
            string direction = "right";
            chessboard.PlaceMines(3); // Place a mine on the player's path

            // Act
            GameState result = GameManager.UpdateGameState(chessboard, player, direction);

            // Assert
            Assert.Equal(GameState.LifeLost, result);
            Assert.Equal(2, player.Lives);
        }

        [Fact]
        public void UpdateGameState_HitMineLastLife_ReturnsLost()
        {
            // Arrange
            IPlayer player = new Player(1);
            IChessboard chessboard = new Chessboard(player, rows: 2, columns: 2); // Set player with 1 life
            string direction = "right";
            chessboard.PlaceMines(3); // Place a mine on the player's path

            // Act
            GameState result = GameManager.UpdateGameState(chessboard, player, direction);

            // Assert
            Assert.Equal(GameState.Lost, result);
        }

        [Fact]
        public void UpdateGameState_WinGame_ReturnsWon()
        {
            // Arrange
            IPlayer player = new Player();
            IChessboard chessboard = new Chessboard(player);
            string direction = "right";

            // Move the player to the last column to win
            for (int i = 0; i < chessboard.Rows - 2; i++)
            {
                GameManager.UpdateGameState(chessboard, player, direction);
            }

            // Act
            GameState result = GameManager.UpdateGameState(chessboard, player, direction);

            // Assert
            Assert.Equal(GameState.Won, result);
        }
    }
}