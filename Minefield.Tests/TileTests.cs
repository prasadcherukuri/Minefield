namespace Minefield.Domain.Tests
{
    public class TileTests
    {
        [Theory]
        [InlineData(true, false)]
        [InlineData(false, true)]
        [InlineData(true, true)]
        public void Tile_Constructor(bool isMine, bool isVisited)
        {
            // Arrange & Act
            Tile tile = new Tile(isMine, isVisited);

            // Assert
            Assert.Equal(isMine, tile.IsMine);
            Assert.Equal(isVisited, tile.IsVisited);
        }
    }
}