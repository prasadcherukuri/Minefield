using Minefield.Core;
using Minefield.Domain;

namespace Minefield.UI
{
    /// <summary>
    /// Represents a game of Minefield.
    /// </summary>
    public class Game : IGame
    {
        private readonly IPlayer player;
        private readonly IChessboard chessboard;

        /// <summary>
        /// Initializes a new instance of the <see cref="Game"/> class.
        /// </summary>
        /// <param name="player">The player object.</param>
        /// <param name="chessboard">The chessboard object.</param>
        /// <param name="gameManager">The game manager responsible for managing game state.</param>
        public Game(IPlayer player, IChessboard chessboard)
        {
            this.player = player;
            this.chessboard = chessboard;
        }

        /// <summary>
        /// Starts and plays the game.
        /// </summary>
        public void Play()
        {
            this.chessboard.PlaceMines(numberOfMines: 30);

            bool gameOver = false;
            GameState gameState = GameState.New;

            while (!gameOver)
            {
                DisplayGameState(gameState);

                Console.WriteLine("Enter a direction (up, down, left, right):");
                string direction = Console.ReadLine().ToLower();

                gameState = GameManager.UpdateGameState(this.chessboard, this.player, direction);

                if (gameState == GameState.Lost || gameState == GameState.Won)
                {
                    gameOver = true;
                    DisplayGameState(gameState);
                }
            }
        }

        /// <summary>
        /// Displays the current game state.
        /// </summary>
        /// <param name="gameState">The current game state.</param>
        void DisplayGameState(GameState gameState)
        {
            Console.Clear(); // Clear the console to update the game state

            DisplayChessboard();

            switch (gameState)
            {
                case GameState.InvalidUserInput:
                    DisplayPlayerStats();
                    Console.WriteLine("Invalid input. Please enter 'up', 'down', 'left', or 'right'.");
                    break;
                case GameState.Won:
                    Console.WriteLine("Congratulations! You reached the other end of the chessboard.");
                    Console.WriteLine("Your final score: " + this.player.CalculateFinalScore());
                    break;
                case GameState.Lost:
                    Console.WriteLine("Alas! You just hit a mine and lost your last life. Game over.");
                    Console.WriteLine("Your final score: " + this.player.CalculateFinalScore());
                    break;
                case GameState.LifeLost:
                    Console.WriteLine("Sorry, you just lost a life.");
                    DisplayPlayerStats();
                    break;
                case GameState.Inprogress:
                case GameState.New:
                    DisplayPlayerStats();
                    break;
                default:
                    Console.WriteLine("Unknown game state.");
                    break;
            }
        }

        /// <summary>
        /// Displays the chessboard.
        /// </summary>
        void DisplayChessboard()
        {
            for (int row = 0; row < this.chessboard.Rows; row++)
            {
                for (int col = 0; col < this.chessboard.Columns; col++)
                {
                    Tile tile = this.chessboard.GetTile(row, col);
                    if (tile.IsVisited)
                    {
                        if (tile.IsMine)
                        {
                            Console.Write("[X]"); // Display visited mine
                        }
                        else
                        {
                            Console.Write("[ ]"); // Display visited empty tile
                        }
                    }
                    else if (row == this.player.CurrentRow && col == this.player.CurrentColumn)
                    {
                        Console.Write("[P]"); // Display the player's position
                    }
                    else
                    {
                        Console.Write("[?]"); // Display unvisited tile
                    }
                }
                Console.WriteLine(); // Move to the next row
            }
        }

        /// <summary>
        /// Displays player statistics.
        /// </summary>
        void DisplayPlayerStats()
        {
            Console.WriteLine($"Player Position: {this.player.CurrentPosition}");
            Console.WriteLine($"Lives left: {this.player.Lives}");
            Console.WriteLine($"Moves taken: {this.player.CalculateFinalScore()}");
        }
    }
}
