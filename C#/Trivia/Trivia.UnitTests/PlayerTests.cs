using FluentAssertions;
using Xunit;

namespace Trivia.UnitTests
{
    public class PlayerTests
    {
        [Fact]
        public void CanAddToPlayerPurse()
        {
            var player = new Player("Tibuleac");
            player.AddToPurse(3);

            player.Purse.Should().Be(3);
        }
    }
}