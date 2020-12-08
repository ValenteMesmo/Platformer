using NSubstitute;
using Platformer.Desktop;
using Xunit;

namespace Tests.Unit
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var obj = Player.Create(new GameInputs());
            obj.UpdateHandler();
            obj.Destroy();
        }
    }
}
