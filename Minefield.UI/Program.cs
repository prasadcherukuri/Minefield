using Microsoft.Extensions.DependencyInjection;
using Minefield.Core;
using Minefield.Domain;

namespace Minefield.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            string userInput = string.Empty;

            var serviceProvider = new ServiceCollection()
                .AddTransient<IPlayer, Player>()
                .AddTransient<IChessboard, Chessboard>()
                .AddTransient<IGame, Game>()
                .BuildServiceProvider();

            while (userInput.Trim().ToLower() != "q")
            {
                var game = serviceProvider.GetRequiredService<IGame>();
                game.Play();

                Console.WriteLine("Press \"q\" to quit or any other key to play another game.");
                userInput = Console.ReadLine();
            }
        }
    }
}
